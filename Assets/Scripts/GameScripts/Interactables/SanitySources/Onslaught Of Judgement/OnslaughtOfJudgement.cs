using System.Collections.Generic;
using UnityEngine;

public class OnslaughtOfJudgement : MonoBehaviour
{
    List<Watcher> Watchers { get; set; } = new();
    
    float TimeTillNextDamage{ get; set; }
    const float TakeDamageInterval = 1;

	PlayerInteraction PlayerInteraction { get; set; }
    PlayerSanityController PlayerSanityController { get; set; }

    void Start()
    {
        PlayerInteraction = FindObjectOfType<PlayerInteraction>();
        PlayerSanityController = FindObjectOfType<PlayerSanityController>();
        PlayerInteraction.CanInteract = false;
	}

	void Update()
    {
        if (Watchers.Count == 0)
            EndOnslaughtOfJudgement();
        else
            HandleDamage();
	}

    void HandleDamage()
    {
        TimeTillNextDamage -= Time.deltaTime;
        if(TimeTillNextDamage < 0)
        {
            TimeTillNextDamage = TakeDamageInterval;
            PlayerSanityController.Sanity -= 5;
        }
    }

    public void AddWatcher(Watcher watcher)
    {
        if(!Watchers.Contains(watcher))
            Watchers.Add(watcher);
	}

	public void RemoveWatcher(Watcher watcher)
	{
		if (Watchers.Contains(watcher))
			Watchers.Remove(watcher);
	}

	void EndOnslaughtOfJudgement()
	{
        PlayerInteraction.CanInteract = true;
        Destroy(gameObject);
	}
}
