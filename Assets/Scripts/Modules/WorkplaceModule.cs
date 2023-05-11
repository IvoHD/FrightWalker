using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New WorkplaceModule", menuName = "Modules/WorkplaceModule")]

public class WorkplaceModule : ScriptableObject, IModule
{
    [field: SerializeField]
    Material moduleMaterial;
    ModuleDeployer ModuleDeployer;
    GameObject[,] Area;
    public List<WorkPlaceModels> models;
    public List<GameObject> Items;
    public string Tag { get; } = "WorkplaceModule";


    public void Setup(ModuleDeployer deployer)
    {
        ModuleDeployer = deployer;
        Area = ModuleDeployer.MainArea;
    }

    public void Deploy()
    {
        GameObject[,] workplaceArea = new GameObject[Area.GetLength(0) - 2, Area.GetLength(1)];
        for (int i = 2; i < Area.GetLength(0); i++)
            for (int y = 0; y < Area.GetLength(1); y++)
            {
                Area[i, y].transform.tag = Tag;
                workplaceArea[i - 2, y] = Area[i, y];
            }
        MoveSeatsPlan(workplaceArea);
    }
    public void MoveSeatsPlan(GameObject[,] workplaceArea)
    {
        for (int i = 0; i < 2; i++)
        {
            bool variant = MathsRand.Instance.Chance(2);
            switch (MathsRand.Instance.RandNumOutOfRange(1, 3))
            {
                case 2:
                    for (int y = 0; y < workplaceArea.GetLength(i) / 3; y++)
                        if (variant)
                            MoveSeatsExecution(i, (workplaceArea.GetLength(i) - y) - 1, workplaceArea);
                        else
                            MoveSeatsExecution(i, y, workplaceArea);
                    break;
                case 3:
                    for (int y = 0; y < workplaceArea.GetLength(i) / 4; y++)
                        if (variant)
                            MoveSeatsExecution(i, (Mathf.FloorToInt((workplaceArea.GetLength(i) / 3) * 2) - 1) + y, workplaceArea);
                        else
                            MoveSeatsExecution(i, Mathf.CeilToInt(workplaceArea.GetLength(i) / 3) + y, workplaceArea);
                    break;
                default:
                    break;
            }
        }

    }

    public void MoveSeatsExecution(int axis, int index, GameObject[,] workplaceArea)
    {
        for (int i = 0; i < workplaceArea.GetLength(1 - axis); i++)
            if (axis == 0)
                workplaceArea[index, i].transform.tag = "GroundObjects";
            else
                workplaceArea[i, index].transform.tag = "GroundObjects";

    }
    public void Mark()
    {
        GameObject[] moduleSpace = GameObject.FindGameObjectsWithTag(Tag);
        foreach (GameObject slot in moduleSpace)
        {
            for (int i = 0; i < slot.transform.childCount; i++)
            {
                if (slot.transform.GetChild(i).name == "CenterPiece")
                {
                    slot.transform.GetChild(i).transform.GetComponent<MeshRenderer>().material = moduleMaterial;
                }
            }
        }
    }
    public void Spawn()
    {

        ModuleSpawner.Instance.WorkPlaceSpawn(this, ModuleDeployer, GetWorkplaceModels());
        ItemSpawner();

    }

    List<WorkPlaceModels> GetWorkplaceModels()
    {
        List<WorkPlaceModels> spawnableObjects = new List<WorkPlaceModels>();
        foreach (WorkPlaceModels modelobject in models)
            spawnableObjects.Add(modelobject);
        return spawnableObjects;
    }
    public void ItemSpawner()
    {
        GameObject[] models = GameObject.FindGameObjectsWithTag("Model");
        foreach (GameObject model in models)
        {
            Transform modelChildren;
            modelChildren = model.transform.GetComponentInChildren<Transform>();
            foreach (Transform modelChild in modelChildren)
                if (modelChild.tag == "ItemNode")
                    if (MathsRand.Instance.Chance(2) || true)
                    {
                        ModuleDeployer.PlaceModels(modelChild.position.x, modelChild.position.y, modelChild.position.z, Items[MathsRand.Instance.RandNumOutOfRange(0, Items.Count-1)]);
                    }
        }
    }
}