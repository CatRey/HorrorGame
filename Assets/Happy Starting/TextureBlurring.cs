using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.MathF;

public class TextureBlurring : MonoBehaviour
{
    public ComputeShader blur;
    public RenderTexture input, result;
    public Camera inputting;
    public RawImage screen;
    [Range(1, 31)]
    public int OFFSETS;

    float GaussianValue(float x, float y)
    {
        return Max(Cos(Sqrt(x * x + y * y)), 0);
    }

    private void Start()
    {
        Vector4[] gaussians = new Vector4[1024];

        gaussians[0] = Vector4.one * GaussianValue(0, 0);
        int OFFHALF = OFFSETS / 2;
        int x = -OFFHALF, y = -OFFHALF;
        for (int i = 0; i < OFFSETS* OFFSETS; i++)
        {
            gaussians[i + 1] = Vector4.one * GaussianValue(x, y);
            x++;
            if (x > OFFHALF)
            {
                y++;
                x = -OFFHALF;
            }
        }

        blur.SetInt("OFFSETS", OFFSETS);

        blur.SetVectorArray("preComputedGaussians", gaussians);
        blur.SetFloat("gaussianSum", Array.ConvertAll(gaussians, x=> x.x).Sum());

        input = new RenderTexture(input);
        input.width = Screen.width;
        input.height = Screen.height;
        input.enableRandomWrite = true;
        inputting.targetTexture = input;

        result = new RenderTexture(input);
        result.enableRandomWrite = true;
        screen.texture = result;

        blur.SetTexture(0, "Input", input);
        blur.SetTexture(0, "Result", result);
    }


    private void Update()
    {
        Vector4[] gaussians = new Vector4[1024];

        gaussians[0] = Vector4.one * GaussianValue(0, 0);
        int OFFHALF = OFFSETS / 2;
        int x = -OFFHALF, y = -OFFHALF;
        for (int i = 0; i < OFFSETS * OFFSETS; i++)
        {
            gaussians[i + 1] = Vector4.one * GaussianValue(x, y);
            x++;
            if (x > OFFHALF)
            {
                y++;
                x = -OFFHALF;
            }
        }
        blur.SetInt("OFFSETS", OFFSETS);

        blur.SetVectorArray("preComputedGaussians", gaussians);
        blur.SetFloat("gaussianSum", Array.ConvertAll(gaussians, x => x.x).Sum());


        blur.Dispatch(0, (int)Ceiling(Screen.width / 8f), (int)Ceiling(Screen.height / 8f), 1);
    }
}
