using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerController : MonoBehaviour
{
    public float hunger;
    public float hungerGainSpeed;
    public static HungerController hungerController;

    public AudioClip small, medium, critical;
    public float mediumHunger, criticalHunger;
    public float updateHungerPeriod;
    float timeForAnUpdate;
    
    private void Start()
    {
        hungerController = this;
    }


    private void Update()
    {
        bool justUpdated = false;

        justUpdated = (hunger <= mediumHunger && hunger + hungerGainSpeed * Time.deltaTime > mediumHunger) || (hunger <= criticalHunger && hunger + hungerGainSpeed * Time.deltaTime > criticalHunger);

        hunger += hungerGainSpeed * Time.deltaTime;

        timeForAnUpdate -= Time.deltaTime;

        if (timeForAnUpdate <= 0)
        {
            timeForAnUpdate = updateHungerPeriod;

            SoundManager.Play(hunger <= mediumHunger ? small : (hunger <= criticalHunger ? medium : critical), Camera.main.transform.position - Camera.main.transform.up, Camera.main.transform);
        }
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
