using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.MathF;

public class HiddenSilhouette : MonoBehaviour
{
    public float maxAngle, destructionAngle, maxDistance, multiplier;
    Transform camera;
    MeshRenderer meshRenderer;
    
    private void Start()
    {
        camera = Camera.main.transform;
        meshRenderer = GetComponent<MeshRenderer>();
    }


    private void Update()
    {
        var vector = transform.position - camera.position;
        var angle = Vector3.Angle(vector, camera.forward);
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, (angle - maxAngle > 0) && (vector.magnitude - maxDistance > 0) ? Min(angle - maxAngle, vector.magnitude - maxDistance)* multiplier : 0);

        if (angle <= destructionAngle)
        {
            Destroy(gameObject);
        }
    }
}
