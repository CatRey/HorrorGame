using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator3D : InteractableInCollider
{
    public List<Overlayable3D> grabables = new();

    public float angleSign;
    public static float rotation;
    public Transform rotatableObject;
    public Vector3 wasVector, projectedPosition;

    private void Start()
    {

        MakeInteractable();
    }

    private void Update()
    {
        rotation = (rotatableObject.eulerAngles.z) * Mathf.Deg2Rad;
    }

    public override void MakeInteractable()
    {
        foreach (var item in grabables)
        {
            item.overlayed = false;
        }
        base.MakeInteractable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(projectedPosition, 0.01f);
    }
}
