using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    PlayerMouseLook PlayerMouseLook { get; set; }

    [field: SerializeField]
    TextMeshProUGUI MouseSensitivityText { get; set; }
    public float MouseSensitivityValue
    {
        get { return float.Parse(MouseSensitivityText.text); }

        set
        {
            if (value >= 1 && value <= 5)
            {
				MouseSensitivityText.text = value.ToString("F2");
                PlayerMouseLook.MouseSensitivity = value;
			}
		}
    }

    [field: SerializeField]
    TextMeshProUGUI FOVText { get; set; }
    public float FOVValue
    {
        get { return float.Parse(FOVText.text); }

        set
        {
            if (value >= 40 && value <= 90)
            {
                FOVText.text = value.ToString("F2");
                PlayerMouseLook.FOV = value;
            }

        }
    }

    [field: SerializeField]
    TextMeshProUGUI SFXText { get; set; }
    [field: SerializeField]
    TextMeshProUGUI MusicText { get; set; }
    [field: SerializeField]
    TextMeshProUGUI MasterText { get; set; }

	[field: SerializeField]
	AudioMixerGroup SFXMixer { get; set; }
	[field: SerializeField]
	AudioMixerGroup MusicMixer { get; set; }
    [field: SerializeField]
    AudioMixerGroup MasterMixer { get; set; }

	public float SFXValue
    {
        get { return float.Parse(SFXText.text); }
        set
        {
            if (value >= 0 && value <= 100)
				SFXText.text = value.ToString();
		}
    }
    public float MusicValue
    {
        get { return float.Parse(MusicText.text); }
        set
        {
            if (value >= 0 && value <= 100)
                MusicText.text = value.ToString();
        }
    }
    public float MasterValue
    {
        get { return float.Parse(MasterText.text); }
        set
        {
            if (value >= 0 && value <= 100)
                MasterText.text = value.ToString();
        }
    }

    public void PlayGame()
    {
        GameManager.Instance.LoadPlayground();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}