using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingManager : MonoBehaviour
{
    RoomManager RM { get; set; }
    RoomStyle RS { get; set; }
    public void Initiate(RoomManager rM, RoomStyle rS)
    {
        RM = rM;
        RS = rS;
        SpawnCeilings(GameObject.FindGameObjectsWithTag("GroundObjects"));
        Debug.Log("We need to build..");

    }
    void SpawnCeilings(GameObject[] ceilingSpaces)
    {
        foreach(GameObject space in ceilingSpaces)
        {
            InstantiateObjectAt(space.transform.position.x, space.transform.position.y+1, space.transform.position.z, RS.Ceilings.Ceilings[MathsRand.Instance.RandNumOutOfRange(0, RS.Ceilings.Ceilings.Count - 1)]);
        }
    }
    GameObject InstantiateObjectAt(float x, float y, float z, GameObject prefab)
    {
        List<GameObject> spawnableObjects = new List<GameObject>();

        GameObject newObject = Instantiate(prefab, new(x, y, z), Quaternion.identity);


        return newObject;
    }
}
