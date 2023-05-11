using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    RoomManager RM { get; set; }
    RoomStyle RS { get; set; }
    public void Initiate(RoomManager rM, RoomStyle rS)
    {
        RM = rM;
        RS = rS;
        SpawnWalls(RM.GetBorderNodes());
        Debug.Log("We need to build..");

    }
    void SpawnWalls(List<Transform> wallNodes)
    {
        foreach (Transform wallNode in wallNodes)
        {
            Debug.Log("We need to build a wall..");
            WallAdjust(wallNode);
        }
    }
    void WallAdjust(Transform node)
    {
        GameObject model = RS.Walls.Walls[MathsRand.Instance.RandNumOutOfRange(0, RS.Walls.Walls.Count-1)];


        if (node.position.x > node.parent.parent.transform.position.x )
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
            model = RS.Walls.Windows[MathsRand.Instance.RandNumOutOfRange(0, RS.Walls.Windows.Count - 1)];
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
    GameObject InstantiateObjectAt(float x, float y, float z, GameObject prefab)
    {
        List<GameObject> spawnableObjects = new List<GameObject>();

        GameObject newObject = Instantiate(prefab, new(x, y, z), Quaternion.identity);


        return newObject;
    }
}