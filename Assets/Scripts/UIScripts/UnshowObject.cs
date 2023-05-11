using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnshowObject : MonoBehaviour
{
    [SerializeField]
    GameObject _gameObject;
    void Start()
    {
       _gameObject.SetActive(false);
    }

    void OnTriggerEnter (Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            _gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            _gameObject.SetActive(true);
        }
    }
}
