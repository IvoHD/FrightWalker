using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayQuickinfoAnimation : MonoBehaviour
{
    [SerializeField]
    Animator _animationController;

    [SerializeField]
    GameObject _textObject;

    void Start()
    {
        _textObject.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            _textObject.SetActive(true);
            _animationController.SetBool("PlayTextAnimation", true);
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            _textObject.SetActive(false);
            _animationController.SetBool("PlayTextAnimation", false);
        }
    }
}
