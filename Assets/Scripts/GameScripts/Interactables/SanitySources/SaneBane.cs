using UnityEngine;

public class SaneBane : MonoBehaviour, ISanitySource, INotifyOnNextRoom
{
    public float TimeLeft { get; private set; }
    public float StartingTime { get; private set; }
    bool HasStarted { get; set; }

    float TickingSpeed { get; set; } = 0.6f;
    [field: SerializeField]
    AudioSource ClockTickingSource { get; set;}

    PlayerSanityController PlayerSanityController { get; set; }
	void Start()
    {
        (this as INotifyOnNextRoom).Subscribe();
        PlayerSanityController = FindObjectOfType<PlayerSanityController>();
		StartBane(20);       
    }

    void Update()
    {
        if (HasStarted)
        {
            DeductTime();
            IncreaseClockTickingInterval();
        }
    }

    void IncreaseClockTickingInterval()
    {
        ClockTickingSource.pitch = TickingSpeed;
        TickingSpeed += Time.deltaTime * 1.5f / StartingTime;
    }

    void DeductTime()
    {
        TimeLeft -= Time.deltaTime;
        if (TimeLeft < 0)
        {
            ClockTickingSource.enabled = false;
            EndSaneBane();
            PlayerSanityController.Sanity = -999999999999999;
        }
	}

    /// <summary>
    /// Starts the SaneBane SanitySource. t in seconds sets the duration of the event.
    /// </summary>
    /// <param name="t"></param>
    public void StartBane(int t)
    {
        AudioManager.Instance.Muted = true;

        HasStarted = true;
        StartingTime = t;
        TimeLeft = t;
        ClockTickingSource.enabled = true;
    }

    public void OnNextRoom()
    {
        EndSaneBane();
    }

    void EndSaneBane()
    {
        AudioManager.Instance.Muted = false;
        (this as INotifyOnNextRoom).Unsubscribe();
        Destroy(gameObject);
    }
}
