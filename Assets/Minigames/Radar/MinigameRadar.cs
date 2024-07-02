using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameRadar : Minigame
{
    public RawImage scene;

    
    private void Start()
    {
        scene.texture = GameObject.FindGameObjectWithTag("2DSceneRenderer").GetComponent<Camera>().targetTexture;
    }
}
