
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;




[CreateAssetMenu(fileName = "New FurnitureModule", menuName = "Modules/FurnitureModule")]


public class FurnitureModule : ScriptableObject, IModule
{
    [field: SerializeField]
    Material moduleMaterial;
    ModuleDeployer ModuleDeployer;
    List<GameObject[,]> Areas;
    [SerializeField]
    public List<SingleModels> models;
    GameObject[] ModuleSpace { get; set; }
    public string Tag { get; } = "FurnitureModule";






    public void Setup(ModuleDeployer deployer)
    {
        ModuleDeployer = deployer;
        Areas = ModuleDeployer.ExtraParts;
    }
    public void Deploy()
    {
        foreach (GameObject[,] area in Areas)
            foreach (GameObject platform in area)
                if (platform != null)
                    platform.transform.tag = "FurnitureModule";

    }

    public void Mark()
    {

        ModuleSpace = ModuleHelper.Instance.ModuleSpace(Tag);
        foreach (GameObject slot in ModuleSpace)
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
        ModuleSpawner.Instance.SingleObjectSpawn(this, ModuleDeployer, SingleModels(modelPossitions.Wall), SingleModels(modelPossitions.Center), ModuleSpace);

    }

   
    public List<SingleModels> SingleModels(modelPossitions spawn)
    {
        List<SingleModels> spawnableObjects = new List<SingleModels>();

        foreach (SingleModels modelobject in models)
            if (modelobject.spawn == spawn)
                spawnableObjects.Add(modelobject);
        return spawnableObjects;
    }
 

}

