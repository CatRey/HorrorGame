using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public MinigameInteractable minigameInteractable;
    public float breakPeriod;
    float timeFromBreak;
    bool wasBroken;
    
    private void Start()
    {
        
    }


    private void Update()
    {
        timeFromBreak += Time.deltaTime;

        if (timeFromBreak >= breakPeriod && !wasBroken)
        {
            OnBroke();
        }
        if (!minigameInteractable.enabled && wasBroken)
        {
            OnFixed();
        }


        wasBroken = timeFromBreak >= breakPeriod;
    }



    public void OnBroke()
    {
        minigameInteractable.enabled = true;
    }
    public void OnFixed()
    {
        timeFromBreak = breakPeriod;
    }
}
