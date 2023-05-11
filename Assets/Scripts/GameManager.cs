using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	List<INotifyOnNextRoom> NotifyOnNextRooms { get; set; } = new();


	public Vector3 PlayerPos { get; set; }

	public static GameManager Instance { get; private set; }
	void Awake()
	{
		if (Instance is not null)
			Destroy(this);
		else
			Instance = this;
	}

	public void AddNotifyOnNextRoom(INotifyOnNextRoom notifyOnNextRoom)
	{
		if(!NotifyOnNextRooms.Contains(notifyOnNextRoom))
			NotifyOnNextRooms.Add(notifyOnNextRoom);
	}

	public void RemoveNotifyOnNextRoom(INotifyOnNextRoom notifyOnNextRoom)
	{
		if (NotifyOnNextRooms.Contains(notifyOnNextRoom))
			NotifyOnNextRooms.Remove(notifyOnNextRoom);
	}

	public void LoadNextRoom()
	{
		foreach (INotifyOnNextRoom notifyOnNextRoom in NotifyOnNextRooms.ToList())
			notifyOnNextRoom.OnNextRoom();

		if (SceneManager.GetActiveScene().buildIndex == 2)
			GetComponent<RoomManager>().GenerateRoom();
		else
		{
			SceneManager.LoadScene(2);
			GetComponent<RoomManager>().GenerateRoom();
		}
	}

	public void LoadShionsRoom()
	{
		SceneManager.LoadScene(1);
		PlayerPos = new(395, 3, -985);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void LoadPlayground()
	{
		PlayerPrefs.SetInt("HasCompletedTutorial", 0);

		SceneManager.LoadScene(1);
		PlayerPos = new(395, 1, -1000);
	}
}
