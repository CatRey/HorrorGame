using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class DimableLight : MonoBehaviour
{
    Light light;
    public float transitionSpeed;
    public AnimationCurve lightByPower;
    public List<BasicBreakable3D> generators = new();

    private void Start()
    {
        light = GetComponent<Light>();
    }

    private void Update()
    {

        int functional = 0;
        foreach (var item in generators)
        {
            if (!item.broken) functional++;
        }

        light.intensity = Mathf.Lerp(light.intensity, lightByPower.Evaluate(functional), Time.deltaTime * transitionSpeed);
    }
}
