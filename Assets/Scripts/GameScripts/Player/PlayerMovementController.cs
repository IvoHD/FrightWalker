using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
	CharacterController CharacterController { get; set; }

	float MovementSpeed { get; set; } = DefaultMovementSpeed;
	float JumpHeight { get; set; } = DefaultJumpHeight;
	const float DefaultMovementSpeed = 5;
	const float DefaultJumpHeight = 5;

	public float Stamina { get; private set; } = 100;
	public bool IsExhausted { get; private set; } = false;
	const int MaxStamina = 100;

	public bool IsSprinting { get; private set; }
	float SprintingSpeedFactor { get; set; } = 1.7f;
	int SprintingStaminaDecay { get; set; } = 33;
	int StaminaRegain { get; set; } = 60;
	bool CanRegainStamina { get; set; }

	float YVelocity { get; set; }
	const float Gravity = -10;

	Vector2 ToMove { get; set; } = Vector2.zero;
	public bool IsMoving { get { return ToMove != Vector2.zero; } }

	[field: SerializeField]
	AudioClip JumpingSound { get; set; }

	[field: SerializeField]
	AudioClip LandingSoundSoft { get; set; }
	[field: SerializeField]
	AudioClip LandingSoundMedium { get; set; }
	[field: SerializeField]
	AudioClip LandingSoundHard { get; set; }

	public bool IsAirborne { get; private set; }

	void Start()
	{
		CharacterController = GetComponent<CharacterController>();
	}

	void Update()
	{
		ApplyGravity();
		Move();
	}

	void Move()
	{
		Vector3 move = MovementSpeed * (transform.right * ToMove.x + transform.up * YVelocity + transform.forward * ToMove.y);

		if (Stamina < 0)
			StartCoroutine(CExhaustForSeconds(5));
		else if (IsSprinting && !IsExhausted)
		{ 
			move *= SprintingSpeedFactor;
			if (IsMoving)
				Stamina -= SprintingStaminaDecay * Time.deltaTime;
		}
		else if(!IsExhausted && CanRegainStamina)
			Stamina += StaminaRegain * Time.deltaTime;

		Stamina = Mathf.Clamp(Stamina, -1, MaxStamina);
		CharacterController.Move(move * Time.deltaTime);
	}

	public void OnMove(InputValue inputValue)
	{
		ToMove = inputValue.Get<Vector2>();
	}

	public void OnJump()
	{
		if (CharacterController.isGrounded)
		{
			YVelocity += JumpHeight;
			AudioManager.Instance.PlaySound(JumpingSound);
		}
	}

	public void OnSprint()
	{
		IsSprinting = !IsSprinting;

		if (IsSprinting)
			CanRegainStamina = false;
		else
			StartCoroutine(CDisableStaminaRegenerationForSeconds(2));
	}

	void ApplyGravity()
	{
		if (!CharacterController.isGrounded)
		{
			IsAirborne = true;
			YVelocity += Gravity * Time.deltaTime;
		}
		else
		{
			if (IsAirborne)
			{
				AudioClip AudioClipToPlay = YVelocity switch
				{
					> -5 => LandingSoundSoft,
					> -12 => LandingSoundMedium,
					< 0 => LandingSoundHard,
				};
				AudioManager.Instance.PlaySound(AudioClipToPlay);
				IsAirborne = false;
			}
			YVelocity = Mathf.Clamp(YVelocity, -1, float.PositiveInfinity);
		}
	}

	/// <summary>
	/// Coroutine that sets player MovementSpeed for t amount of seconds
	/// </summary>
	/// <param name="speed">New speed Value</param>
	/// <param name="t">Time in seconds</param>
	public IEnumerator CSetMovementSpeedForSeconds(int speed, int t)
	{
		MovementSpeed = speed;

		yield return new WaitForSeconds(t);

		MovementSpeed = DefaultMovementSpeed;
	}


	/// <summary>
	/// Coroutine that prevents the player from regaining stamina for t amount seconds
	/// </summary>
	/// <param name="t">Time in seconds</param>
	public IEnumerator CExhaustForSeconds(int t)
	{
		Stamina = 0;
		IsExhausted = true;
		StartCoroutine(CSetMovementSpeedForSeconds(1, t));

		yield return new WaitForSeconds(t);

		IsExhausted = false;
	}

	/// <summary>
	/// Coroutine that disables the player from regaining stamina for t amount seconds
	/// </summary>
	/// <param name="t">Time in seconds</param>
	public IEnumerator CDisableStaminaRegenerationForSeconds(int t)
	{
		yield return new WaitForSeconds(t);

		CanRegainStamina = true;
	}
}