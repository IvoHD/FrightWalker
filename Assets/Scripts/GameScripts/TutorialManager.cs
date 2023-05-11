using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	[field: SerializeField]
	TextMeshProUGUI KeyPromptVerb { get; set; }

	[field: SerializeField]
    TextMeshProUGUI KeyPromptText { get; set; }

	[field: SerializeField]
	Image KeyPromptIcon { get; set; }

	PlayerMovementController PlayerMovementController { get; set; }

	KeyPromptSO _currentKeyPrompt;

	GameObject SoccerBall { get; set; }
	GameObject VolleyBall { get; set; }
	Vector3 SoccerBallStartingPos { get; set; }
	Vector3 VolleyBallStartingPos { get; set; }

	public KeyPromptSO CurrentKeyPrompt
	{
		get { return _currentKeyPrompt; }
		private set 
		{
			_currentKeyPrompt = value;
			KeyPromptIcon.sprite = value.Icon;
			KeyPromptText.text = value.Text;
			KeyPromptVerb.text = value.Verb;
		}
	}

	[field: SerializeField]
	List<KeyPromptSO> KeyPrompts { get; set; }

	public Condition CurrCondition { get; private set; }

	void Start()
    {
		PlayerMovementController = GetComponent<PlayerMovementController>();

		SoccerBall = GameObject.Find("SoccerBall");
		VolleyBall = GameObject.Find("VolleyBall");

		SoccerBallStartingPos = SoccerBall.transform.position;
		VolleyBallStartingPos = VolleyBall.transform.position;

		CurrentKeyPrompt = KeyPrompts[0];
	}

	/// <summary>
	/// Sets the current condition to the next codition and sets current keyprompt to next keypromt if the current condition matches the specified condition.
	///	If the current condition does not match the specified condition, no action is taken.
	/// </summary>
	/// <param name="condition">The condition to compare against the current condition.</param>
	public void NextConditionIfCurrent(Condition condition)
	{
		if(CurrCondition == condition)
		{
			CurrCondition++;
			NextKeyprompt();
		}
	}

	/// <summary>
	/// Advances to the next keyPrompt in the KeyPrompts list. If the current key prompt is the last one in the list this object gets destroyed.
	/// </summary>
	public void NextKeyprompt()
	{
		int nextIndex = KeyPrompts.IndexOf(CurrentKeyPrompt) + 1;

		if (nextIndex >= KeyPrompts.Count)
		{
			KeyPromptIcon.enabled = false;
			KeyPromptVerb.enabled = false;
			KeyPromptText.enabled = false;
			Destroy(this);

			return;
		}

		CurrentKeyPrompt = KeyPrompts[nextIndex];
	}

	public void OnMove()
	{
		NextConditionIfCurrent(Condition.HasMoved);
	}

	public void OnSprint()
	{
		if (PlayerMovementController.IsMoving)
			NextConditionIfCurrent(Condition.HasSprinted);
	}

	public void OnDrop()
	{
		if (SoccerBall.transform.position != SoccerBallStartingPos && SoccerBall.transform.parent == null)
			NextConditionIfCurrent(Condition.HasDropped);
		else if (VolleyBall.transform.position != VolleyBallStartingPos && VolleyBall.transform.parent == null)
			NextConditionIfCurrent(Condition.HasDropped);
	}
}