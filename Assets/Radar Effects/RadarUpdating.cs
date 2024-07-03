using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarUpdating : MonoBehaviour
{
    public RawImage radarImage;
    public ComputeShader shader;
    public float rotatingSpeed, angle;
    public float fadeSpeed = 1;
    public float seeInnacuracy = 0.01f;
    public RenderTexture input;
    public RenderTexture target;
    public Color found = Color.blue, forgot = Color.green;
    private void Start()
    {
        input = new RenderTexture(input);
        input.enableRandomWrite = true;
        GameObject.FindGameObjectWithTag("2DSceneRenderer").GetComponent<Camera>().targetTexture = input;
        target = new RenderTexture(target);
        target.enableRandomWrite = true;
        radarImage.texture = target;

        shader.SetFloat("seeInnacuracy", seeInnacuracy);
        shader.SetVector("foundColor", found);
        shader.SetVector("forgotColor", forgot);
        shader.SetTexture(0, "Input", input);
        shader.SetTexture(0, "Result", target);
    }


    private void Update()
    {
        angle = (angle + rotatingSpeed * Time.deltaTime) % (Mathf.PI*2);
        shader.SetFloat("angle", angle);
        shader.SetFloat("fadeSpeed", fadeSpeed * Time.deltaTime);
        shader.Dispatch(0, 128, 128, 1);
    }
}
