using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimableMaterialEmission : MonoBehaviour
{
    public Material material;
    public new MeshRenderer renderer;


    public float transitionSpeed;
    public Color[] activeStatesColors;
    public List<BasicBreakable3D> generators = new();
    Color nowColor;

    private void Start()
    {
        material = new Material(material);
        material.color = material.color;
        renderer.material = material;
    }


    private void Update()
    {
        int functional = 0;
        foreach (var item in generators)
        {
            if (!item.broken) functional++;
        }

        nowColor = Color.Lerp(nowColor, activeStatesColors[functional], Time.deltaTime * transitionSpeed);


        renderer.material.SetColor("_EmissionColor", nowColor);
    }
}
