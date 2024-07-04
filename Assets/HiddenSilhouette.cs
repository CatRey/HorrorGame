using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.MathF;

public class HiddenSilhouette : MonoBehaviour
{
    public float maxAngle, destructionAngle, maxDistance, multiplier;
    Transform camera;
    MeshRenderer meshRenderer;
    public float shakeTime, shakeIntensity, disableTime;
    bool work = true;
    
    private void Start()
    {
        camera = Camera.main.transform;
        meshRenderer = GetComponent<MeshRenderer>();
    }


    private void Update()
    {
        if (!work) return;
        var vector = transform.position - camera.position;
        var angle = Vector3.Angle(vector, camera.forward);
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, (angle - maxAngle > 0) && (vector.magnitude - maxDistance > 0) ? Min(angle - maxAngle, vector.magnitude - maxDistance)* multiplier : 0);

        if (angle <= destructionAngle)
        {
            StartCoroutine(Scare());
            work = false;
        }
        if (vector.sqrMagnitude <= maxDistance * maxDistance)
        {
            StartCoroutine(Scare());
            work = false;
        }
    }

    public IEnumerator Scare()
    {
        var shaker = camera.GetComponent<CameraShaker>();
        shaker.direction = (transform.position - camera.position).normalized;
        shaker.intensity = Max(shaker.intensity, shakeIntensity);
        shaker.shakingTime = Max(shaker.shakingTime, shakeTime);
        PlayerDisabler.playerDisabler.DisablePlayer();
        while (disableTime > 0)
        {
            if (!PlayerDisabler.playerDisabler.disabled)
            {
                PlayerDisabler.playerDisabler.DisablePlayer();
            }
            disableTime -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        PlayerDisabler.playerDisabler.EnablePlayer();
        Destroy(gameObject);
    }
}
