using UnityEngine;

public class Altar : MonoBehaviour, IInteractable
{
    Vector3 NodePosition { get; set; }
    GameObject Item { get; set; } = null;

    PlayerHandController PlayerHandController { get; set; }

    void Start()
    {
        NodePosition = transform.GetChild(0).gameObject.transform.position;
        PlayerHandController = FindObjectOfType<PlayerHandController>();
	}

    public void Interact()
    {
        if(Item is null && PlayerHandController.ItemInHand is not null)
        {
            Item = PlayerHandController.ItemInHand;
            PlayerHandController.RemoveAndPositionAt(NodePosition);
        }
    }

}
