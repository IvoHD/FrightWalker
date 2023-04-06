using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	List<INotifyOnNextRoom> NotifyOnNextRooms { get; set; } = new();

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
	}
}
