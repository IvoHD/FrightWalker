using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingManager : MonoBehaviour
{
    RoomStyle RoomStyle { get; set; }
    
    public void Initiate(RoomStyle roomStyle)
    {
        RoomStyle = roomStyle;
        SpawnCeilings(GameObject.FindGameObjectsWithTag("GroundObjects"));
    }

    /// <summary>
    /// Spawns ceilings
    /// </summary>
    /// <param name="ceilingSpaces"></param>
    void SpawnCeilings(GameObject[] ceilingSpaces)
    {
        foreach(GameObject space in ceilingSpaces)
            InstantiateObjectAt(space.transform.position.x, space.transform.position.y + 1.5f, space.transform.position.z, RoomStyle.Ceilings.Ceilings[MathsRand.Instance.RandNumOutOfRange(0, RoomStyle.Ceilings.Ceilings.Count - 1)]);
    }

    /// <summary>
    /// Instantiate object at position
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    GameObject InstantiateObjectAt(float x, float y, float z, GameObject prefab)
    {
		return Instantiate(prefab, new(x, y, z), Quaternion.identity); ;
    }
}
