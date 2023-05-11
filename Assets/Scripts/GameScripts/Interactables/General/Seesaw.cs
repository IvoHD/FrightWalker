using UnityEngine;

public class Seesaw : MonoBehaviour
{
	TutorialManager TutorialManager { get; set; }

	Vector3 RLeanXRotation { get; } = new(0, 0, 0);
	Vector3 LLeanXRotation { get; } = new(25, 0, 0);

	bool IsLeanedRight { get; set; } = false;

	[field: SerializeField]
	GameObject Seesawseats { get; set; }

	[field: SerializeField]
	SphereCollider RCollider { get; set; }
	[field: SerializeField]
	SphereCollider LCollider { get; set; }

	void Start()
	{
		TutorialManager = FindObjectOfType<TutorialManager>();
		RCollider.enabled = false;
	}

	void Update()
	{
		Seesawseats.transform.localEulerAngles = Vector3.Lerp(Seesawseats.transform.localEulerAngles, IsLeanedRight ? RLeanXRotation : LLeanXRotation, Time.deltaTime * 2f);
	}

	void OnTriggerEnter(Collider other)
	{
		RCollider.enabled = !RCollider.enabled;
		LCollider.enabled = !LCollider.enabled;
		IsLeanedRight = !IsLeanedRight;
		TutorialManager.NextConditionIfCurrent(Condition.HasJumped);
	}
}
