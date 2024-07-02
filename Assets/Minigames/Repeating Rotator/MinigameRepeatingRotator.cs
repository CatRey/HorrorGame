using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.MathF;

public class MinigameRepeatingRotator : Minigame
{
    public RectTransform rotating;
    public int rotateAmount;
    int rotated;
    public float maxAngle;
    float nowAngle;
    int nowAngleSign = 1;
    Vector3 previousVector;

    private void Start()
    {
        previousVector = Input.mousePosition - rotating.position;
    }


    private void Update()
    {

        var nowVector = Input.mousePosition - rotating.position;

        float nowAngled = Vector3.SignedAngle(previousVector, nowVector, Vector3.forward);
        Debug.Log(nowAngled);

        if (Sign(nowAngle) == nowAngleSign && Abs(nowAngle + nowAngled) >= maxAngle)
        {
            rotating.eulerAngles += Vector3.forward * Sign(nowAngled) * (maxAngle - Abs(nowAngle));
            nowAngle = maxAngle * nowAngleSign;
            nowAngleSign *= -1;

            rotated++;
            if (rotated >= rotateAmount)
            {
                invoker.MinigameStopped(true);
            }
        }
        else if (nowAngleSign == Sign(nowAngled))
        {
            nowAngle += nowAngled;
            rotating.eulerAngles += Vector3.forward * nowAngled;
        }


        previousVector = nowVector;


        base.Update();
    }
}
