using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameLeverPulling : Minigame
{
    public Button[] levers;
    public Color turnedOnColor, turnedOffColor;
    public bool[] turnedOn;
    
    private void Start()
    {
        turnedOn = new bool[levers.Length];
    }


    private void Update()
    {
        base.Update();
    }

    public void SwitchLever(int index)
    {
        turnedOn[index] = !turnedOn[index];

        levers[index].image.color = turnedOn[index] ? turnedOnColor : turnedOffColor;
        levers[index].GetComponentInChildren<Text>().text = turnedOn[index] ? "ON" : "OFF";

        if (Array.TrueForAll(turnedOn, x => x))
        {
            invoker.MinigameStopped(true);
        }

    }
}
