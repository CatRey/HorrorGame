using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.MathF;

public class HiddenSilhouette : MonoBehaviour
{
    public float maxAngle, destructionAngle, maxDistance, multiplier;
    Transform camera;
    MeshRenderer[] meshRenderers;
    List<MaterialColor> materialColors = new();
    public float shakeTime, shakeIntensity, disableTime;
    bool work = true;
    
    private void Start()
    {
        camera = Camera.main.transform;
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        materialColors.AddRange(Array.ConvertAll(meshRenderers, x => new MaterialColor() { original = x.material.color, renderer = x }));
    }


    private void Update()
    {
        if (!work) return;
        var vector = transform.position - camera.position;
        var angle = Vector3.Angle(vector, camera.forward);
        foreach (var item in materialColors)
        {
            item.renderer.material.color = new Color(item.original.r, item.original.g, item.original.b, (angle - maxAngle > 0) && (vector.magnitude - maxDistance > 0) ? Min(angle - maxAngle, vector.magnitude - maxDistance) * multiplier : 0);
        }

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

    public struct MaterialColor
    {
        public MeshRenderer renderer;
        public Color original;
    }
}
