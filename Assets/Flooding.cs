using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flooding : MonoBehaviour
{
    public int crackAmount;
    public AnimationCurve floodSpeedPerCracks;
    public Transform water;
    
    private void Start()
    {
        
    }


    private void Update()
    {
        water.transform.localScale += Vector3.up * floodSpeedPerCracks.Evaluate(crackAmount) * Time.deltaTime;
    }

    public void CrackCreated()
    {
        crackAmount++;
    }

    public void CrackFixed()
    {
        crackAmount--;
    }
}
