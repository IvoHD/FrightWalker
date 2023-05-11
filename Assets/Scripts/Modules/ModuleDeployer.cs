
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ModuleDeployer : MonoBehaviour
{

    public GameObject[,] MainArea { get; set; }
    public List<GameObject[,]> ExtraParts { get; set; }
    public RoomStyle RoomStyle { get; set; }
    public RoomManager RoomManager { get; set; }

    /// <summary>
    /// Deploys all modules
    /// </summary>
    public void DeployModules()
    {
        foreach (IModule module in RoomStyle.Modules)
        {
            module.Setup(this);
            module.Deploy();
            module.Mark();
            module.Spawn();
        }

    }
    public GameObject PlaceModels(float x, float y, float z, GameObject prefab)
    {
       return InstantiateObjectAt(x, y, z, prefab);
    }

    GameObject InstantiateObjectAt(float x, float y, float z, GameObject prefab)
    {
        List<GameObject> spawnableObjects = new List<GameObject>();

        GameObject newObject = Instantiate(prefab, new(x, y, z), Quaternion.identity);


        return newObject;
    }


}
public class ModuleHelper
{
    private static ModuleHelper instance = null;
    private ModuleHelper() { } // private constructor to prevent instantiation from outside

    public static ModuleHelper Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ModuleHelper();
            }
            return instance;
        }
    }


    public GameObject[] ModuleSpace(string tag)
    {
        GameObject[] moduleSpace = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject gameObject in moduleSpace)
        {
            gameObject.transform.tag = "GroundObjects";
        }
        return moduleSpace; 
        

    }
    public List<Transform> GetNodes(GameObject parrent)
    {
        List<Transform> nodes = new();
        foreach (Transform child in parrent.GetComponentsInChildren<Transform>(true))
            if (child.CompareTag("NodeContainer"))
                foreach (Transform childNode in child.GetComponentsInChildren<Transform>(true))
                    nodes.Add(childNode);
        return nodes;
    }
    public bool IsBorderGroundObject(RoomManager roomManager, GameObject parrent)
    {
        foreach (Transform node in ModuleHelper.Instance.GetNodes(parrent))
            if (!roomManager.IsPlaced(node))
                return true;
        return false;

    }
    public Transform FirstWallDirerctionNode(RoomManager roomManager, GameObject parrent)
    {
        foreach (Transform node in ModuleHelper.Instance.GetNodes(parrent))
            if (!roomManager.IsPlaced(node))
                return node;
        return null;

    }
}
[Serializable]
public class SingleModels
{
    public GameObject gameObject;
    public modelPossitions spawn;
}
[Serializable]
public class WorkPlaceModels
{
    public SingleModels BackModel;
    public SingleModels FrontModel;
    [Range(0, 5)]
    public int AlignStatus;
}

public enum modelPossitions
{
    Wall,
    Center,
    WorkplaceBack,
    WorkplaceFront
}
public class ModuleSpawner
{
    private static ModuleSpawner instance = null;
    private ModuleSpawner() { } // private constructor to prevent instantiation from outside

    public static ModuleSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ModuleSpawner();
            }
            return instance;
        }
    }
    public void SingleObjectSpawn(IModule module, ModuleDeployer mD,List<SingleModels> wall, List<SingleModels> center, GameObject[] moduleSpace)
    {

        foreach (GameObject square in moduleSpace)
            if (MathsRand.Instance.Chance(3))
            {
                GameObject models;


                if (ModuleHelper.Instance.IsBorderGroundObject(mD.RoomManager, square))
                {
                    models = wall[MathsRand.Instance.RandNumOutOfRange(0, wall.Count - 1)].gameObject;
                    models.tag = "Model";

                    WallAdjust(mD.PlaceModels(square.transform.position.x, square.transform.position.y, square.transform.position.z, models), square, mD);
                }
                else
                {
                    models =  center[MathsRand.Instance.RandNumOutOfRange(0, center.Count - 1)].gameObject;
                    models.tag = "Model";
                   mD.PlaceModels(square.transform.position.x, square.transform.position.y, square.transform.position.z, models);
                }

            }
        
    }
    public void WorkPlaceSpawn(IModule module, ModuleDeployer mD, List<WorkPlaceModels> model)
    {
        GameObject[] moduleSpace = ModuleHelper.Instance.ModuleSpace(module.Tag);
        foreach (GameObject square in moduleSpace)
        {
            WorkPlaceModels workplace = model[MathsRand.Instance.RandNumOutOfRange(0, model.Count - 1)];
            GameObject frontModel = workplace.FrontModel.gameObject;
            GameObject backModel = workplace.BackModel.gameObject;
            
            frontModel.tag = "Model";
            backModel.tag = "Model";

            WorkplaceAjust(mD.PlaceModels(square.transform.position.x, square.transform.position.y, square.transform.position.z,frontModel), mD.PlaceModels(square.transform.position.x, square.transform.position.y, square.transform.position.z, backModel), workplace.AlignStatus);
        }
;


    }
    void WorkplaceAjust(GameObject frontModel,GameObject backModel,int aling)
    {
        float alingrepositionPos = MathsRand.Instance.RandNumOutOfRange(0, aling)/50;
        if (MathsRand.Instance.Chance(2))
            alingrepositionPos *= -1;
        float alingrepositionRot = MathsRand.Instance.RandNumOutOfRange(0, aling)*10;
        if (MathsRand.Instance.Chance(2))
            alingrepositionRot *= -1;
        frontModel.transform.position += new Vector3(alingrepositionPos, 0, (float)0.25);
      
        backModel.transform.position += new Vector3(alingrepositionPos, 0, (float)-0.25);
        backModel.transform.Rotate(0, alingrepositionRot, 0);
    }
    void WallAdjust(GameObject model, GameObject square, ModuleDeployer mD)
    {
        Vector3 node = ModuleHelper.Instance.FirstWallDirerctionNode(mD.RoomManager, square).position;



        if (square.transform.position.z > node.z)
        {
            model.transform.Rotate(0, 270, 0);
            model.transform.position += new Vector3(0, 0, (float)-0.5);

        }
        else if (square.transform.position.z < node.z)
        {
            model.transform.Rotate(0, 90, 0);
            model.transform.position += new Vector3(0, 0, (float)0.5);
        }

        else if (square.transform.position.x < node.x)
        {
            model.transform.Rotate(0, 180, 0);
            model.transform.position += new Vector3((float)0.5, 0, 0);
        }
        else
        {

            model.transform.position += new Vector3((float)-0.5, 0, 0);

        }
    }
}
