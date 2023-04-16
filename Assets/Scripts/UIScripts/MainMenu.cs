using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [field: SerializeField]
    TextMeshProUGUI MouseSensitivityText { get; set; }
    public float MouseSensitivityValue
    {
        get { return float.Parse(MouseSensitivityText.text); }

        set
        {
            if (value >= 1 && value <= 5)
                MouseSensitivityText.text = value.ToString("F2");
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
                FOVText.text = value.ToString();

        }
    }

    [field: SerializeField]
    TextMeshProUGUI SFXText { get; set; }
    [field: SerializeField]
    TextMeshProUGUI MusicText { get; set; }
    [field: SerializeField]
    TextMeshProUGUI MasterText { get; set; }

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}