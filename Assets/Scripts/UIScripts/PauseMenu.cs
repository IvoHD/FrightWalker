using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	DisplayPauseMenu DisplayPauseMenu { get; set; }

	void Start()
	{
		DisplayPauseMenu = GetComponent<DisplayPauseMenu>();
	}

	public void Continue()
	{
		DisplayPauseMenu.OnPause();
	}

	public void ToMainMenu()
	{
		GameManager.Instance.LoadMainMenu();
	}
}
