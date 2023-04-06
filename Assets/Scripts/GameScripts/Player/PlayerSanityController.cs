using UnityEngine;

public class PlayerSanityController : MonoBehaviour
{
	[field: SerializeField]
	AudioClip DeathClip { get; set; }

	float _sanity = 100;

	public float Sanity
	{
		get { return _sanity; }
		set
		{
			_sanity = value;

			if (_sanity > 100)
				_sanity = 100;
			if (_sanity <= 0)
				AudioManager.Instance.PlaySound(DeathClip);
		}
	}
}
