using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    RoomManager RoomManager { get; set; }
    RoomStyle RoomStyle { get; set; }

    public void Initiate(RoomManager roomManager, RoomStyle roomStyle)
    {
        RoomManager = roomManager;
        RoomStyle = roomStyle;
        SpawnWalls(RoomManager.GetBorderNodes());
    }

    void SpawnWalls(List<Transform> wallNodes)
    {
        int isExit = 0;
        int isEntrance = 0;

		while (isExit == isEntrance)
        {
			isExit = MathsRand.Instance.RandNumOutOfRange(0, wallNodes.Count + 1);
			isEntrance = MathsRand.Instance.RandNumOutOfRange(0, wallNodes.Count + 1);
		}
         

        for (int i = 0; i < wallNodes.Count; i++)
            WallAdjust(wallNodes[i], i == isEntrance, i == isExit);
    }

    void WallAdjust(Transform node, bool IsEntrance, bool IsExit)
    {
        GameObject model = RoomStyle.Walls.Walls[MathsRand.Instance.RandNumOutOfRange(0, RoomStyle.Walls.Walls.Count-1)];

        if (IsEntrance)
            model = RoomStyle.Walls.Entrance;
        else if(IsExit)
			model = RoomStyle.Walls.Exit;

		if (node.position.x > node.parent.parent.transform.position.x)
        {
            model = InstantiateObjectAt(node.position.x, node.position.y, node.position.z, model);
            model.transform.Rotate(0, 270, 0);
            model.transform.position += new Vector3((float)-0.5, 0, 0 );

        }
        else if (node.position.x < node.parent.parent.transform.position.x)
        {
            model = InstantiateObjectAt(node.position.x, node.position.y, node.position.z, model);
            model.transform.Rotate(0, 90, 0);
            model.transform.position += new Vector3((float)0.5, 0, 0);
        }
        else if (node.position.z > node.parent.parent.transform.position.z)
        {
            if(!IsEntrance && !IsExit)
                model = RoomStyle.Walls.Windows[MathsRand.Instance.RandNumOutOfRange(0, RoomStyle.Walls.Windows.Count - 1)];
            model = InstantiateObjectAt(node.position.x, node.position.y, node.position.z, model);
            model.transform.Rotate(0, 180, 0);
            model.transform.position += new Vector3(0, 0, (float)-0.5);
        }
        else
        {
            model = InstantiateObjectAt(node.position.x, node.position.y, node.position.z, model);
            model.transform.position += new Vector3(0, 0, (float)0.5);
        }
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