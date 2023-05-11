using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
	GameObject _ItemInHand;
    public GameObject ItemInHand 
	{
		get { return _ItemInHand; }
		set 
		{
			if (_ItemInHand is not null)
				_ItemInHand.transform.parent = null;

			_ItemInHand = value;

			if (_ItemInHand is not null)
			{
				_ItemInHand.transform.parent = Hand.transform;
				_ItemInHand.transform.localScale *= ItemInHand.GetComponent<IItem>().PickupScaleFactor;
				_ItemInHand.transform.localPosition = Vector3.zero;
				_ItemInHand.transform.localRotation = Quaternion.identity;
			}
		}
	}

    [field: SerializeField]
    GameObject Hand { get; set; }

	[field: SerializeField]
	AudioClip[] PickupAndDropClips { get; set; } = new AudioClip[3];

	/// <summary>
	/// Adds item to player hand.
	/// If player hand is occupied or gameobject has not IItem component no action is taken. 
	/// </summary>
	/// <param name="item">Item to be added</param>
	/// <returns></returns>
	public void AddItem(GameObject item)
	{
		if (ItemInHand is null && item.GetComponent<IItem>() is not null)
		{
			ItemInHand = item;
			Rigidbody rigidbody = ItemInHand.GetComponent<Rigidbody>();
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			AudioManager.Instance.PlaySound(PickupAndDropClips[Random.Range(0, 3)]);
		}
	}
 
    public void OnUse()
    {
        if (ItemInHand is not null)
        {
			ItemInHand.GetComponent<IItem>().Use();
			AudioManager.Instance.PlaySound(ItemInHand.GetComponent<IItem>().UseClip);
			ItemInHand.transform.localScale /= ItemInHand.GetComponent<IItem>().PickupScaleFactor;
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