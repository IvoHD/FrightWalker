using UnityEngine;

public class Swing : MonoBehaviour, IInteractable
{
    TutorialManager TutorialManager { get; set; }
	Ball[] Balls { get; set; }

	Vector3 RotationOrigin { get; set; } = Vector3.zero;
	Vector3 CurrentRotationGoal { get; set; } = Vector3.zero;
	
	Vector3 MaxRotation { get; } = new(40, 0, 0);

	bool IsSwinging { get; set; }


	float RotationTimer { get; set; } = RotationInterval;
	const float RotationInterval = 0.6f;

    void Start()
    {
		TutorialManager = FindObjectOfType<TutorialManager>();
		Balls = FindObjectsOfType<Ball>();
	}

	void Update()
	{
		HandleSwinging();
	}

	void HandleSwinging()
	{
		if (!IsSwinging)
			return;

		gameObject.transform.eulerAngles = Vector3.Lerp(CurrentRotationGoal, RotationOrigin, RotationTimer / RotationInterval);
		RotationTimer -= Time.deltaTime;

		if (RotationTimer < 0)
		{
			RotationOrigin = CurrentRotationGoal; // Set new rotation origin to the previous goal
			CurrentRotationGoal = -1 * (CurrentRotationGoal - (CurrentRotationGoal.x > 0 ? new Vector3(5, 0, 0) : new Vector3(-5, 0, 0)));
			RotationTimer = RotationInterval;
		}
		else if (Mathf.Approximately(CurrentRotationGoal.x, 0))
		{
			IsSwinging = false;
		}
	}

	public void Interact()
	{
		CurrentRotationGoal = MaxRotation;
		IsSwinging = true;

		foreach (Ball ball in Balls)
			ball.enabled = true;

		TutorialManager.NextConditionIfCurrent(Condition.HasInteracted);
	}
}
