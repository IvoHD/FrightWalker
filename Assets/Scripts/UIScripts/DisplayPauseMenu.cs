using UnityEngine;

public class DisplayPauseMenu : MonoBehaviour
{
	public bool IsPaused { get; private set; }

	[field: SerializeField]
	GameObject PauseCanvas { get; set; }

	public void OnPause()
	{
		IsPaused = !IsPaused;
		TogglePauseMenu();
	}

	void TogglePauseMenu()
	{
		Time.timeScale = IsPaused ? 0f : 1f;
		PauseCanvas.active = IsPaused;
	}
}
