using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherHeadMovement : MonoBehaviour
{
	GameObject Player { get; set; }

	void Start()
    {
        Player = GameObject.Find("Player");
	}

	void Update()
    {
        transform.LookAt(Player.transform);
	}
}
