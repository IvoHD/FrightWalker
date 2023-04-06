using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    public GameObject ItemInHand { get; private set; }
    [field: SerializeField]
    GameObject Hand { get; set; }

	[field: SerializeField]
	AudioClip[] PickupAndDropClips { get; set; } = new AudioClip[3]; 

	/// <summary>
	/// Adds item to player hand. Returns true if item was added successful.
	/// </summary>
	/// <param name="item">Item to be added</param>
	/// <returns></returns>
	public void AddItem (GameObject item)
    {
        if (ItemInHand is null && item.GetComponent<IItem>() is not null)
        {
            ItemInHand = item;
			AudioManager.Instance.PlaySound(PickupAndDropClips[Random.Range(0, 3)]);
            SetItemInHand();
        }
    }

    void SetItemInHand()
    {
        ItemInHand.transform.localScale *= ItemInHand.GetComponent<IItem>().PickupScaleFactor;
		ItemInHand.transform.parent = Hand.transform;
		ItemInHand.transform.localPosition = Vector3.zero;
		ItemInHand.transform.localRotation = Quaternion.identity;
	}

    public void OnUse()
    {
        if (ItemInHand is not null)
        {
			ItemInHand.GetComponent<IItem>().Use();
			AudioManager.Instance.PlaySound(ItemInHand.GetComponent<IItem>().UseClip);
			ItemInHand = null;
		}
    }

    public void RemoveAndPositionAt(Vector3 position)
    {
        if (ItemInHand is not null)
		{
			ItemInHand.transform.localScale /= ItemInHand.GetComponent<IItem>().PickupScaleFactor;
			ItemInHand.transform.parent = null;
			ItemInHand.transform.position = new(position.x, position.y + ItemInHand.transform.lossyScale.y / 2, position.z);
			ItemInHand.transform.localRotation = Quaternion.identity;
			ItemInHand = null;

			AudioManager.Instance.PlaySound(PickupAndDropClips[Random.Range(0, 3)]);
		}
	}
}