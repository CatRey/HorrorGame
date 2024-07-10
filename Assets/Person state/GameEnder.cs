using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    public PassiveDying passiveDying;
    public Rotator3D rotator3D;
    public bool active;
    public float transitionTime;
    float timeTransitioning;

    public int titlesScene;

    private void Start()
    {
        
    }


    private void Update()
    {
        if (active)
        {
            timeTransitioning += Time.deltaTime;
            passiveDying.drowning.multiplier = Mathf.Lerp(passiveDying.normalMultiplier, passiveDying.drownedMultiplier, timeTransitioning / transitionTime);
            passiveDying.drowning.multiplier += passiveDying.H;
            passiveDying.drowning.multiplier *= passiveDying.drowning.multiplier;
            passiveDying.drowning.multiplier = passiveDying.V / passiveDying.drowning.multiplier;
            passiveDying.drowning.multiplier -= passiveDying.V / passiveDying.H / passiveDying.H;

            if (timeTransitioning >= transitionTime)
            {
                SceneManager.LoadScene(titlesScene);
            }
        }
    }

    public void OnEnteredEnd()
    {
        active = true;
        passiveDying.enabled = rotator3D.enabled = false;
        passiveDying.drowning.enabled = true;
    }
}
