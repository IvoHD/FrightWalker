using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public interface IModule
{
    string Tag { get; }
    void Setup(ModuleDeployer deployer);
    void Deploy();
    void Mark();
    void Spawn();
}
public interface IWorkplaces
{
    void ItemSpawner(GameObject workplace, List<GameObject> items, GameObject square);
}