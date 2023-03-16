using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour, IItem
{
	//PlayerHandController HandController;
	PlayerMovement PlayerMovement;

	public float PickupScaleFactor { get; } = 0.75f;

	void Start()
	{
		//HandController = GameObject.Find("Player").GetComponent<PlayerHandController>();
 		PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();	
	}

	public void Interact()
	{
		PlayerHandController.Instance.AddItem(gameObject);
			
	}

	public void Use()
	{
		StartCoroutine(PlayerMovement.CSetMovementSpeedForSeconds(15, 3));
		Destroy(gameObject);
	}
}
