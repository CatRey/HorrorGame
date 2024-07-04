using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicBreakable3D : Breakable3D
{
    public UnityEvent onBroke, onFixed;


    public override void OnBroke()
    {
        onBroke.Invoke();
        base.OnBroke();
    }

    public override void OnFixed()
    {
        onFixed.Invoke();
        base.OnFixed();
    }
}
