using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RoomStyle", menuName = "RoomStyle")]
public class RoomStyle : ScriptableObject
{
    public Material GroundMaterial;
    public GameObject Buildingblock;
    public Material MainroomMaterial;
    public List<ScriptableObject> Modules;
    public Wall Walls;
    public Ceiling Ceilings;
    [Range(5, 10)]
    public int RoomSize;
    [Range(0, 4)]
    public int Parts;
}
