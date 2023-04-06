using UnityEngine;

public class Watcher : MonoBehaviour, IInteractable
{
    OnslaughtOfJudgement OnslaughtOfJudgement { get; set; }

    void Start()
    {
        OnslaughtOfJudgement = FindObjectOfType<OnslaughtOfJudgement>();
        OnslaughtOfJudgement.AddWatcher(this);
    }

    public void Interact()
	{
        OnslaughtOfJudgement.RemoveWatcher(this);
        Destroy(gameObject);
	}
}
