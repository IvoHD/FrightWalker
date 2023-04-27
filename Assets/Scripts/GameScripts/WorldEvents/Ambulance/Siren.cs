using System;
using UnityEngine;

public class Siren : MonoBehaviour
{
    Light BlueLight { get; set; }
    Light RedLight { get; set; }

    float SirenToggleTimer { get; set; } = SirenToggleInterval;
    const float SirenToggleInterval = 0.4f;

    void Start()
    {
        BlueLight = GameObject.Find("BlueLight").GetComponent<Light>();
        RedLight = GameObject.Find("RedLight").GetComponent<Light>();

        BlueLight.enabled = true;
        RedLight.enabled = false;
    }

    void Update()
    {
        HandleSiren();
    }

    void HandleSiren()
    {
        SirenToggleTimer -= Time.deltaTime;

        if(SirenToggleTimer < 0)
        {
            SirenToggleTimer = SirenToggleInterval;
            ToggleSiren();
        }
    }

    void ToggleSiren()
    {
        BlueLight.enabled = !BlueLight.enabled;
        RedLight.enabled = !RedLight.enabled;
    }
}
