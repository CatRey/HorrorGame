using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerController : MonoBehaviour
{
    public float hunger;
    public float hungerGainSpeed;
    public static HungerController hungerController;
    
    private void Start()
    {
        hungerController = this;
    }


    private void Update()
    {
        hunger += hungerGainSpeed * Time.fixedDeltaTime;
    }

    public void ReduceHungerLocal(float howMuch)
    {
        hunger = Mathf.Max(0, hunger - howMuch);
    }

    public static void ReduceHunger(float howMuch)
    {
        hungerController.ReduceHungerLocal(howMuch);
    }
}
