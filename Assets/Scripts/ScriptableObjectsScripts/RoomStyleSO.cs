using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomStyle", menuName = "ScriptableObjects/RoomStyle")]
public class RoomStyleSO : ScriptableObject
{
	//Room Properties
	[field: SerializeField] public List<RoomObjectData> Floors { get; private set; } = new List<RoomObjectData>();
	[field: SerializeField] public List<RoomObjectData> Workplaces { get; private set; } = new List<RoomObjectData>();
	[field: SerializeField] public List<RoomObjectData> Furnitures { get; private set; } = new List<RoomObjectData>();
	[field: SerializeField] public List<RoomObjectData> GroundInsanitySorces { get; private set; } = new List<RoomObjectData>();
	[field: SerializeField] public List<RoomObjectData> Walls { get; private set; } = new List<RoomObjectData>();
	[field: SerializeField] public RoomObjectData Door {get; private set; } = new RoomObjectData();
	[field: SerializeField] public List<RoomObjectData> Ceilings { get; private set; } = new List<RoomObjectData>();
	[field: SerializeField] public List<RoomObjectData> CeilingsLights { get; private set; } = new List<RoomObjectData>();
	[field: SerializeField] public int SanityScoreMax { get; private set; } = new int();
	[field: SerializeField] public int SanityScoreMin { get; private set; } = new int();
	[field: SerializeField, Range(0, 10)] public int DangerLevel { get; private set; } = new int();
	[field: SerializeField] public RoomSize Size { get; private set; } = new int();

	void OnEnable()
	{
		foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
			if (propertyInfo.PropertyType == typeof(List<RoomObjectData>))
				if (!PercentagesAddUpTo100(propertyInfo.GetValue(this) as List<RoomObjectData>))
					throw new ArgumentException(propertyInfo.Name + " percentages do not add up to 100 percent");
	}

	bool PercentagesAddUpTo100(List<RoomObjectData> objects)
	{
		float sum = new float();

		foreach(RoomObjectData obj in objects)
			sum += obj.SpawnRate;

		return sum == 100;
	}
}

[Serializable]
public partial class RoomObjectData
{
	[field: SerializeField] public GameObject Obj { get; private set; }
	[field: SerializeField] public float SpawnRate { get; private set; }
}