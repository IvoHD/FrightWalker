using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	[field: SerializeField]
	Transform CameraTransform;

	public void OnInteract()
	{
		if (Physics.Raycast(new(CameraTransform.position, CameraTransform.forward), out RaycastHit hit, 5f, LayerMask.GetMask("Collision")))
		{
			IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();
			if (interactable is not null)
				interactable.Interact();
		}
	}

	public void OnDrop()
	{
		if (CameraTransform.localRotation.eulerAngles.x < 90 && CameraTransform.localRotation.eulerAngles.x > 20 && Physics.Raycast(new(CameraTransform.position, CameraTransform.forward), out RaycastHit hit, 5f, LayerMask.GetMask("Collision")))
			PlayerHandController.Instance.RemoveAndPositionAt(hit.point);
	}
}
