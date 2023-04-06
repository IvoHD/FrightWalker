using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
	public void Interact()
	{
		GameManager.Instance.LoadNextRoom();
	}
}
