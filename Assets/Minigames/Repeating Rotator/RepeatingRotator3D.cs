using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingRotator3D : InteractableInCollider
{
    public Overlayable3D isSelected;
    public Transform rotating, driving;
    public float angleSign, angleToRotate;
    public float angleDirection;
    public float angleRotated;
    public Vector3 projectedPosition;
    public Vector3 wasVector;


    private void Start()
    {
        base.Start();
    }


    private void Update()
    {
        
    }

    public override void MakeInteractable()
    {
        base.MakeInteractable();
        angleDirection = Random.Range(0, 2) * 2 - 1;
        angleRotated = 0;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(projectedPosition, 0.01f);
    }
}
