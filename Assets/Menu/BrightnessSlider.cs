using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    public Slider brightness;
    public Image brightnessPanel;
    public static float brightnessSave = 0.61f;

    private void Start()
    {

        brightness.value = brightnessSave;
    }


    private void Update()
    {
    }

    public void OnBrightnessChanged()
    {
        brightnessPanel.color = new Color(0, 0, 0, 1 - brightness.value);
        brightnessSave = brightness.value;
    }
}
