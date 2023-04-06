using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrazyFrenzy : MonoBehaviour, IInteractable, IItem
{
	public float PickupScaleFactor { get; } = 0.75f;

	PlayerHandController PlayerHandController { get; set; }
	PlayerSanityController PlayerSanityController { get; set; }
	PlayerMovementController PlayerMovementController { get; set; }
	CharacterController CharacterController { get; set; }
	PlayerInput PlayerInput { get; set; }
	Transform CameraTransform { get; set; }

	Quaternion RotateDestination { get; set; }
	float TimeTillNextTurn { get; set; } = TurnInterval;
	const float TurnInterval = 3;

	[field: SerializeField]
	public AudioClip UseClip { get; private set; }

	const int CrazyFrenzyDuration = 15;
	void Start()
	{
		PlayerHandController = FindObjectOfType<PlayerHandController>();
		PlayerSanityController = FindObjectOfType<PlayerSanityController>();
		PlayerMovementController = FindObjectOfType<PlayerMovementController>();
		CharacterController = FindObjectOfType<CharacterController>();
		PlayerInput = FindObjectOfType<PlayerInput>();
		CameraTransform = GameObject.Find("PlayerCamera").transform;
	}

	void Update()
	{
		if (!PlayerInput.enabled)
		{
			CameraTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0)); ;
			HandleCrazyFrenzyMovement();
			HandleCrazyFrenzyTurning();
		}
	}

	void HandleCrazyFrenzyMovement()
	{
		CharacterController.Move(CharacterController.transform.forward * 20 * Time.deltaTime);
	}

	void HandleCrazyFrenzyTurning()
	{
		TimeTillNextTurn -= Time.deltaTime;
		PlayerHandController.transform.rotation = Quaternion.Lerp(PlayerHandController.transform.rotation, RotateDestination, 0.1f);

		if (TimeTillNextTurn < 0)
		{
			TimeTillNextTurn = TurnInterval;
			RotateDestination = Quaternion.Euler(0, Random.Range(-180, 180), 0);
		}
	}

	public void Interact()
	{
		PlayerHandController.AddItem(gameObject);
	}

	public void Use()
	{
		PlayerSanityController.Sanity -= 15;
		StartCoroutine(StartCrazyFrenzy());
	}

	IEnumerator StartCrazyFrenzy()
	{
		PlayerInput.enabled = false;
		StartCoroutine(PlayerMovementController.CSetMovementSpeedForSeconds(12, 12));
		yield return new WaitForSeconds(CrazyFrenzyDuration);

		Destroy(gameObject);
		PlayerInput.enabled = true;
	}
}
