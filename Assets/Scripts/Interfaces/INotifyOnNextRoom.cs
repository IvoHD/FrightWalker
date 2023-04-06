public interface INotifyOnNextRoom
{
	void Subscribe()
	{
		GameManager.Instance.AddNotifyOnNextRoom(this);
	}

	void Unsubscribe() 
	{
		GameManager.Instance.RemoveNotifyOnNextRoom(this);
	}

	void OnNextRoom();
}