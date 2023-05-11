using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [field: SerializeField]
    RoomStyle Style { get; set; }
    ModuleDeployer ModuleDeployer { get; set; }
    GameObject PartBuilder { get; set; }
    Part Part { get; set; }
    GameObject WallBuilder { get; set; }
    WallManager WallManager { get; set; }
    GameObject CeilingBuilder { get; set; }
    CeilingManager CeilingManager { get; set; }
    public List<GameObject[,]> RoomParts { get; set; }
    public List<Transform> MainRoomBorder { get; set; }

    Hashtable CheckIfPlaced { get; set; }
    Material GroundMaterial { get; set; }

    void Start()
    {
        GenerateRoom();
    }

    /// <summary>
    /// Generates a room and deletes the current one if it exists
    /// </summary>
    public void GenerateRoom()
    {
        DeleteRoom();
        RoomParts = new List<GameObject[,]>();
        CheckIfPlaced = new Hashtable();
        CreateBuilders();
        BuildRoom();
        CreateModuleDeployerAndWalls();
    }

    /// <summary>
    /// Create builders necessary for room generation
    /// </summary>
    void CreateBuilders()
    {
        PartBuilder = new GameObject("PartBuilder");
        Part = PartBuilder.AddComponent<Part>();
        PartBuilder.tag = "Builder";
        Part.Initiate(this, Style);
        WallBuilder = new GameObject("WallBuilder");
        WallManager = WallBuilder.AddComponent<WallManager>();
        WallBuilder.tag = "Builder";
        CeilingBuilder = new GameObject("WallBuilder");
        CeilingManager = CeilingBuilder.AddComponent<CeilingManager>();
        CeilingBuilder.tag = "Builder";

    }

    void BuildRoom()
    {
        Vector3 builderCord = new(0, 0, 0);
        GroundMaterial = Style.MainroomMaterial;
        foreach (GameObject[,] part in RoomParts)
        {
            RoomAssembler(part, builderCord);
            if (builderCord.magnitude == 0)
                MainRoomBorder = GetBorderNodes();
            builderCord = MainRoomBorder[MathsRand.Instance.RandNumOutOfRange(0, MainRoomBorder.Count - 1)].position;
        }

    }


    void CreateModuleDeployerAndWalls()
    {
        ModuleDeployer = PartBuilder.AddComponent<ModuleDeployer>();
        ModuleDeployer.RoomManager = this;
        ModuleDeployer.MainArea = RoomParts[0];
        ModuleDeployer.ExtraParts = new List<GameObject[,]>();

        if (RoomParts.Count > 1)
            for (int i = 1; i < RoomParts.Count; i++)
                ModuleDeployer.ExtraParts.Add(RoomParts[i]);

        ModuleDeployer.RoomStyle = Style;
        ModuleDeployer.DeployModules();
        WallManager.Initiate(this, Style);
        CeilingManager.Initiate(Style);
    }


    /// <summary>
    /// Deletes current room
    /// </summary>
    void DeleteRoom()
    {
        List<GameObject> roomComponents = GetAllRoomComponents();
        foreach (GameObject roomComponent in roomComponents)
            DestroyImmediate(roomComponent);
        Destroy(GameObject.FindWithTag("Builder"), 0.0f);
    }

    /// <summary>
    /// Assembles the room
    /// </summary>
    /// <param name="part"></param>
    /// <param name="buildCords"></param>
    void RoomAssembler(GameObject[,] part, Vector3 buildCords)
    {
        foreach (Transform child in Style.Buildingblock.transform)
            if (child.name == "CenterPiece")
                child.transform.GetComponent<MeshRenderer>().material = GroundMaterial;

        for (int x = 0; x < part.GetLength(0); x++)
            for (int z = 0; z < part.GetLength(1); z++)
                part[x, z] = InstantiateObjectAt(x + buildCords.x, 0, z + buildCords.z, Style.Buildingblock);
        GroundMaterial = Style.GroundMaterial;
    }

    /// <summary>
    /// Gets all RoomComponents in scene
    /// </summary>
    /// <returns></returns>
    List<GameObject> GetAllRoomComponents()
    {
        List<GameObject> gameObjects = new();
        List<GameObject> groundObjects = new();

        groundObjects.AddRange(GameObject.FindGameObjectsWithTag("GroundObjects"));
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
    /// Instantiate object at
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
            CheckIfPlaced.Add(newObject.transform.position, newObject);
        else
            DestroyImmediate(newObject);

        return newObject;
    }

    public bool IsPlaced(Transform groundObject)
    {
        return CheckIfPlaced.ContainsKey(groundObject.position);
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


