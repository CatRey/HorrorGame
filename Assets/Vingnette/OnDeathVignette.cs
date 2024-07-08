using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathVignette : MonoBehaviour
{
    public VingnetteGenerator vingnette;
    public float startValue, valueReductionSpeed;
    public static bool Died;

    private void Start()
    {
        if (Died)
        {
            Cursor.visible = true;
            vingnette.enabled = true;
            vingnette.multiplier = startValue;
        }
    }


    private void Update()
    {
        if (Died)
        {
            vingnette.multiplier -= vingnette.multiplier / valueReductionSpeed * Time.deltaTime;
        }
    }
}
