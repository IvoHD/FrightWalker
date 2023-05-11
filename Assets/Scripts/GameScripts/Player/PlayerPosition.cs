using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public void Start()
    {
        gameObject.transform.position = GameManager.Instance.PlayerPos; 
    }
}
