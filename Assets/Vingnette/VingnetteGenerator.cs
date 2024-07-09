using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VingnetteGenerator : MonoBehaviour
{
    public ComputeShader shader;
    public RenderTexture renderTexture;
    public RawImage image;
    public Color clear, fill;
    public float multiplier;

    bool loaded;
    private void Start()
    {
        renderTexture = new RenderTexture(renderTexture);
        renderTexture.enableRandomWrite = true;
        renderTexture.width = Screen.width;
        renderTexture.height = Screen.height;
        image.texture = renderTexture;

        shader.SetFloat("distMultiplier", multiplier);
        shader.SetTexture(0, "Tex", renderTexture);
        shader.SetVector("clearColor", clear);
        shader.SetVector("fadeColor", fill);
    }


    private void Update()
    {
        shader.SetFloat("distMultiplier", multiplier);
        shader.Dispatch(0, Mathf.CeilToInt(Screen.width / 8f), Mathf.CeilToInt(Screen.height / 8f), 1);

        if (loaded)
        {
            image.color = Color.white;
            loaded = false;
        }
    }

    private void OnEnable()
    {
        image.enabled = loaded = true;
        image.color = clear;
        shader.SetFloat("distMultiplier", multiplier);
        shader.Dispatch(0, Mathf.CeilToInt(Screen.width / 8f), Mathf.CeilToInt(Screen.height / 8f), 1);
    }
}
