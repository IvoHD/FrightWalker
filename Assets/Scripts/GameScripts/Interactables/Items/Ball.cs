using UnityEngine;

public class Ball : MonoBehaviour, IItem
{
    [field: SerializeField]
    public AudioClip UseClip { get; }
    public float PickupScaleFactor { get; } = 1.2f;

    PlayerHandController PlayerHandController { get; set; }
    TutorialManager TutorialManager { get; set; }

    [field: SerializeField]
    Transform CameraTransform { get; set; }

    const float ThrowForce = 500f;

    void Start()
    {
        PlayerHandController = FindObjectOfType<PlayerHandController>();
        TutorialManager = FindObjectOfType<TutorialManager>();
	}

	public void Interact()
	{
		PlayerHandController.AddItem(gameObject);
		TutorialManager.NextConditionIfCurrent(Condition.HasPickedUp);
	}

	public void Use()
	{
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.constraints = RigidbodyConstraints.None;
		rigidbody.useGravity = true;
		rigidbody.AddForce(CameraTransform.forward * ThrowForce);
		TutorialManager.NextConditionIfCurrent(Condition.HasUsedItem);
	}
}
