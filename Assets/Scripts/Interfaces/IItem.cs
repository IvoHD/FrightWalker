public interface IItem : IInteractable {
	public float PickupScaleFactor { get; }
	public void Use();
}