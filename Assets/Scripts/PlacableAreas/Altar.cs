using UnityEngine;

public class Altar : MonoBehaviour, IInteractable
{
    Vector3 NodePosition { get; set; }
    GameObject Item { get; set; } = null;

    void Start()
    {
        NodePosition = transform.GetChild(0).gameObject.transform.position;
    }

    public void Interact()
    {
        if(Item is null && PlayerHandController.Instance.ItemInHand is not null)
        {
            Item = PlayerHandController.Instance.ItemInHand;
            PlayerHandController.Instance.RemoveAndPositionAt(NodePosition);
        }
    }

}
