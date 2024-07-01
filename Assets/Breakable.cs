using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public MinigameInteractable minigameInteractable;
    public float breakPeriod;
    float timeFromBreak;
    bool wasBroken;

    protected virtual void Start()
    {
        
    }


    protected virtual void Update()
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



    public virtual void OnBroke()
    {
        minigameInteractable.enabled = true;
    }
    public virtual void OnFixed()
    {
        timeFromBreak = 0;
    }
}
