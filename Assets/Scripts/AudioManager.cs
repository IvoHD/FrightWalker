using System;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [field: SerializeField]
    AudioSource WalkingAudioSource { get; set; }
	[field: SerializeField]
	AudioSource RunningAudioSource { get; set; }	
    [field: SerializeField]
	AudioSource GeneralAudioSource { get; set; }
	[field: SerializeField]
	AudioSource HeavyBreathing { get; set; }
	[field: SerializeField]
    PlayerMovementController PlayerMovementController { get; set; }

    public bool Muted { get; set; }

	public static AudioManager Instance { get; private set; }
	void Awake()
	{
		if (Instance is not null)
			Destroy(this);
		else
			Instance = this;
	}

    void Start()
    {
        PlayerMovementController = FindObjectOfType<PlayerMovementController>();
    }

    void Update()
    {
        if(!Muted)
        {
            HandleStepsAudio();
            HadleBreathingAudio();
        }
    }

    void HadleBreathingAudio()
    {
        if(PlayerMovementController.IsExhausted)
			HeavyBreathing.volume = 1;
		else
			HeavyBreathing.volume = 0;
	}

	public void PlaySound(AudioClip clip)
    {
        if(!Muted)
            GeneralAudioSource.PlayOneShot(clip);
    }

    void HandleStepsAudio()
    {
		WalkingAudioSource.volume = 0;
		RunningAudioSource.volume = 0;

        if (PlayerMovementController.IsExhausted)
            return;

        if (PlayerMovementController.IsMoving && PlayerMovementController.IsSprinting)
            RunningAudioSource.volume = 1;
        else if (PlayerMovementController.IsMoving)
            WalkingAudioSource.volume = 1;
    }
}
