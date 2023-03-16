using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	CharacterController CharacterController { get; set; }
	float MovementSpeed { get; set; } = DefaultMovementSpeed;
	const float DefaultMovementSpeed = 5;
	float JumpHeight { get; set; } = DefaultJumpHeight;
	const float DefaultJumpHeight = 5;
	Vector3 ToMove { get; set; } = Vector3.zero;

	public float Stamina { get; private set; } = 100;
	const int MaxStamina = 100;
	public bool IsExhausted { get; private set; } = false;

	bool IsSprinting { get; set; }
	float SprintingSpeedFactor { get; set; } = 1.7f;
	int SprintingStaminaDecay { get; set; } = 33;
	int StaminaRegain { get; set; } = 60;
	bool CanRegainStamina { get; set; }

	public float YVelocity { get; set; }
	const float Gravity = -10;

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
		{
			StartCoroutine(CExhaustForSeconds(5));
		}
		else if (IsSprinting && !IsExhausted)
		{ 
			move *= SprintingSpeedFactor;
			if (move != Vector3.zero)
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
			YVelocity += JumpHeight;
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
			YVelocity += Gravity * Time.deltaTime;
		else
			YVelocity = Mathf.Clamp(YVelocity, -1, float.PositiveInfinity);
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