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
		if (PlayerPrefs.GetInt("HasCompletedTutorial") == 1)
			EndTutorial();

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
		if (CurrCondition == condition && CurrCondition == Condition.HasUsedItem)
		{
			EndTutorial();
			GameManager.Instance.LoadShionsRoom();
		}
		else if (CurrCondition == condition)
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
		CurrentKeyPrompt = KeyPrompts[KeyPrompts.IndexOf(CurrentKeyPrompt) + 1];
	}

	/// <summary>
	/// Ends tutorial and disables keyprompts
	/// </summary>
	void EndTutorial()
	{
		PlayerPrefs.SetInt("HasCompletedTutorial", 1);
		Destroy(KeyPromptText);
		Destroy(KeyPromptVerb);
		Destroy(KeyPromptIcon);
		Destroy(this);
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