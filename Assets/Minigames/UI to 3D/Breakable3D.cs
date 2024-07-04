using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable3D : MonoBehaviour
{
    public InteractableInCollider minigameInteractable;
    public float breakPeriod;
    float timeFromBreak;
    bool wasBroken;

    public bool broken
    {
        get => timeFromBreak >= breakPeriod;
    }

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
        if (!minigameInteractable.canInteract && wasBroken)
        {
            OnFixed();
        }


        wasBroken = timeFromBreak >= breakPeriod;
    }

    public void Break()
    {
        timeFromBreak = breakPeriod * 2;
        OnBroke();
    }


    public virtual void OnBroke()
    {
        minigameInteractable.MakeInteractable();
    }
    public virtual void OnFixed()
    {
        minigameInteractable.MakeUninteractable();
        timeFromBreak = 0;
    }
}
