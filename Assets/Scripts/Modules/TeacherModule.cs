
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;


[CreateAssetMenu(fileName = "New TeacherModule", menuName = "Modules/TeacherModule")]


public class TeacherModule : ScriptableObject, IModule
{
    [field: SerializeField]
    Material moduleMaterial;
    ModuleDeployer ModuleDeployer;
    GameObject[,] Area;
    public List<WorkPlaceModels> models;
    public string Tag { get; } = "TeacherModule";



    public void Setup(ModuleDeployer deployer)
    {
        ModuleDeployer = deployer;
        Area = ModuleDeployer.MainArea;
    }
    public void Deploy()
    {
        int teacherModuleLenght = (int)Mathf.Round(Area.GetLength(1) / 3);
        int  moduleStartCord = 0;
        ///What style 3 styles start middle end
        switch (MathsRand.Instance.RandNumOutOfRange(1, 3))
        {
            case 2: moduleStartCord = MathsRand.Instance.Chance(2)? teacherModuleLenght + teacherModuleLenght/2: teacherModuleLenght;
                break;
            case 3: moduleStartCord = Area.GetLength(1)-teacherModuleLenght;
                break;
            default:
                break;
        }
        for (int i = 0; i < teacherModuleLenght; i++)
            Area[0, moduleStartCord + i].transform.tag = Tag;
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

    }

    List<WorkPlaceModels> GetWorkplaceModels()
    {
        List<WorkPlaceModels> spawnableObjects = new List<WorkPlaceModels>();
        foreach (WorkPlaceModels modelobject in models)
        {
            spawnableObjects.Add(modelobject);
        
        }
        return spawnableObjects;
    }
}
