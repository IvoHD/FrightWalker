using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	[field: SerializeField]
	Transform CameraTransform;
	PlayerHandController PlayerHandController { get; set; }

	public bool CanInteract { get; set; } = true;

	void Start()
	{
		PlayerHandController = GetComponent<PlayerHandController>();	
	}

	void Update()
	{
		if(!CanInteract)
		{
			if (Physics.SphereCast(CameraTransform.position, 1f, CameraTransform.forward, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Watcher")))
			{
				Watcher watcher = hit.transform.gameObject.GetComponent<Watcher>();
				if (watcher is not null)
					watcher.Interact();
			}
		}
	}

	public void OnInteract()
	{
		if(!CanInteract)
			return;

		if (Physics.Raycast(new(CameraTransform.position, CameraTransform.forward), out RaycastHit hit, 5f, LayerMask.GetMask("Collision")))
		{
			IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();
			if (interactable is not null)
				interactable.Interact();
		}
	}

	public void OnDrop()
	{
		if (!CanInteract)
			return;
		if (CameraTransform.localRotation.eulerAngles.x < 90 && CameraTransform.localRotation.eulerAngles.x > 20 && Physics.Raycast(new(CameraTransform.position, CameraTransform.forward), out RaycastHit hit, 5f, LayerMask.GetMask("Collision")))
			PlayerHandController.RemoveAndPositionAt(hit.point);
	}
}
