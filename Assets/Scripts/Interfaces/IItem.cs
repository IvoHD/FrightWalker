using UnityEngine;

public interface IItem : IInteractable {
	public AudioClip UseClip { get; }
	public float PickupScaleFactor { get; }
	public void Use();
}