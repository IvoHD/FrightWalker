using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Walls", menuName = "Walls")]
public class Wall : ScriptableObject
{
    public List<GameObject> Walls;
    public List<GameObject> Windows;
    public GameObject Entrance;
    public GameObject Exit;
}
