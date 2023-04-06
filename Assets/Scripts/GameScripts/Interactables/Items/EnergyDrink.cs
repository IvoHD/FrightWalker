using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour, IItem
{
	PlayerMovementController PlayerMovementController { get; set; }
	PlayerHandController PlayerHandController { get; set; }
	[SerializeField]
	public AudioClip UseClip { get; private set; }

	public float PickupScaleFactor { get; } = 0.75f;

	void Start()
	{
 		PlayerMovementController = FindObjectOfType<PlayerMovementController>();
		PlayerHandController = FindObjectOfType<PlayerHandController>();
	}

	public void Interact()
	{
		PlayerHandController.AddItem(gameObject);
	}

	public void Use()
	{
		StartCoroutine(PlayerMovementController.CSetMovementSpeedForSeconds(15, 3));
		Destroy(gameObject);
	}
}
