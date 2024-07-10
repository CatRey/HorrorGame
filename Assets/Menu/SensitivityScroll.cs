using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityScroll : MonoBehaviour
{
    public UniversalLook universalLook;
    public Slider sensitivity;
    public static float sensitivitySave = 9f;

    private void Start()
    {
        sensitivity.value = sensitivitySave;
    }

    public void OnSensitivityChanged()
    {
        universalLook.senitivityHor = universalLook.senitivityVert = sensitivity.value;
    }
}
