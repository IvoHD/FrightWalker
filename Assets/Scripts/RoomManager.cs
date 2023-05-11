using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomManager : MonoBehaviour
{
    [field: SerializeField]
    RoomStyle Style { get; set; }
    ModuleDeployer modules { get; set; }
    GameObject pB { get; set; }
    Part p { get; set; }
    GameObject wB { get; set; }
    WallManager w { get; set; }
    GameObject cB { get; set; }
    CeilingManager c { get; set; }
    public List<GameObject[,]> roomParts { get; set; }
    public List<Transform> MainRoomBorder { get; set; }



    Hashtable checkIfPlaced;

    Material groundMaterial;

    void Start()
    {
        GenerateRoom();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            DeleteRoom();
            GenerateRoom();
        }
    }

    void GenerateRoom()
    {

        roomParts = new List<GameObject[,]>();
        checkIfPlaced = new Hashtable();
        CreateBuilders();
        BuildRoom();
        CreateModuleDeployerAndWalls();
    }

    void CreateBuilders()
    {
        pB = new GameObject("PartBuilder");
        p = pB.AddComponent<Part>();
        pB.tag = "Builder";
        p.Initiate(this, Style);
        wB = new GameObject("WallBuilder");
        w = wB.AddComponent<WallManager>();
        wB.tag = "Builder";
        cB = new GameObject("WallBuilder");
        c = cB.AddComponent<CeilingManager>();
        cB.tag = "Builder";

    }

    void BuildRoom()
    {
        Vector3 builderCord = new(0, 0, 0);
        groundMaterial = Style.MainroomMaterial;
        foreach (GameObject[,] part in roomParts)
        {
            RoomAsembler(part, builderCord);
            if (builderCord.magnitude == 0)
                MainRoomBorder = GetBorderNodes();
            builderCord = MainRoomBorder[MathsRand.Instance.RandNumOutOfRange(0, MainRoomBorder.Count - 1)].position;
        }

    }

    void CreateModuleDeployerAndWalls()
    {
        modules = pB.AddComponent<ModuleDeployer>();
        modules.RM = this;
        modules.MainArea = roomParts[0];
        modules.ExtraParts = new List<GameObject[,]>();
        if (roomParts.Count > 1)
            for (int i = 1; i < roomParts.Count; i++)
            {
                modules.ExtraParts.Add(roomParts[i]);
            }
        modules.RoomStyle = Style;
        modules.DeployModules();
        w.Initiate(this, Style);
        c.Initiate(this, Style);
    }

    void DeleteRoom()
    {
        List<GameObject> roomComponents = GetRoomComponents();
        foreach (GameObject roomComponent in roomComponents)
            DestroyImmediate(roomComponent);
        Destroy(GameObject.FindWithTag("Builder"), 0.0f);
    }

    void RoomAsembler(GameObject[,] part, Vector3 buildCords)
    {
        foreach (Transform child in Style.Buildingblock.transform)
            if (child.name == "CenterPiece")
                child.transform.GetComponent<MeshRenderer>().material = groundMaterial;

        for (int x = 0; x < part.GetLength(0); x++)
            for (int z = 0; z < part.GetLength(1); z++)
                part[x, z] = InstantiateObjectAt(x + buildCords.x, 0, z + buildCords.z, Style.Buildingblock);
        groundMaterial = Style.GroundMaterial;
    }

    List<GameObject> GetRoomComponents()
    {
        List<GameObject> gameObjects = new();
        List<GameObject> groundObjects = new();

        groundObjects.AddRange(GameObject.FindGameObjectsWithTag("GroundObjects"));
        //Find way to automate this 
        groundObjects.AddRange(GameObject.FindGameObjectsWithTag("TeacherModule"));
        groundObjects.AddRange(GameObject.FindGameObjectsWithTag("WorkplaceModule"));
        groundObjects.AddRange(GameObject.FindGameObjectsWithTag("FurnitureModule"));
        groundObjects.AddRange(GameObject.FindGameObjectsWithTag("Model"));
        groundObjects.AddRange(GameObject.FindGameObjectsWithTag("Item"));
        groundObjects.AddRange(GameObject.FindGameObjectsWithTag("WallObjects"));
        groundObjects.AddRange(GameObject.FindGameObjectsWithTag("CeilingObjects"));




        gameObjects.AddRange(groundObjects);

        return gameObjects;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">x cordiantes</param>
    /// <param name="y">y cordiantes</param>
    /// <param name="z">z cordiantes</param>
    /// <param name="prefab">the to be instantiated prefab</param>
    /// <returns>the GameObject that has been Instantiated</returns>
    GameObject InstantiateObjectAt(float x, float y, float z, GameObject prefab)
    {
        GameObject newObject = Instantiate(prefab, new(x, y, z), Quaternion.identity);

        if (!IsPlaced(newObject.transform))
            checkIfPlaced.Add(newObject.transform.position, newObject);
        else
            DestroyImmediate(newObject);

        return newObject;
    }

    public bool IsPlaced(Transform groundObject)
    {
        return checkIfPlaced.ContainsKey(groundObject.position);
    }
    /// <summary>
    /// Finds all Groundobjects and then searches the children of them for nodes
    /// if a node is not where a GroundObject is already placed it will be added into the list "freeSpaces"
    /// </summary>
    /// <returns> "freeSpaces" are all unocupied free nodes on the outside of the room</returns>
    public List<Transform> GetBorderNodes()
    {
        List<Transform> freeSpaces = new();
        foreach (GameObject groundObject in GameObject.FindGameObjectsWithTag("GroundObjects"))
            foreach (Transform nodeContainer in groundObject.GetComponentsInChildren<Transform>(true))
                if (nodeContainer.CompareTag("NodeContainer"))
                    foreach (Transform childNode in nodeContainer.GetComponentsInChildren<Transform>(true))
                        if (!IsPlaced(childNode))
                            freeSpaces.Add(childNode);

        return freeSpaces;
    }

}


