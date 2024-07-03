using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsanityController : MonoBehaviour
{
    public float insanityMeter;
    public float constantInsanitySpeed;
    public Breakable[] breakables;
    public AnimationCurve insanityPerBreaks;

    public float insaneToSillhouette;
    public GameObject sillhouettePrefab;
    public Vector2 sillhouetteTimePeriod;
    float timeForSillhouette;
    public Vector3 spawnSillhouetteZoneCenter;
    public Vector2 spawnSillhouetteZoneSize;
    public float insaneToShake;
    public CameraShaker cameraShaker;
    public Vector2 shakePeriod, shakeTime, shakeIntensity;
    [Range(0,1)]
    public float chanceToDirectionalShake;
    float timeForShake;
    
    private void Start()
    {
        
    }


    private void Update()
    {
        insanityMeter += constantInsanitySpeed * Time.deltaTime;

        int brokenAmount = 0;
        foreach (var breakable in breakables)
        {
            if (breakable.broken) brokenAmount++;
        }
        insanityMeter += insanityPerBreaks.Evaluate(brokenAmount) * Time.deltaTime;

        if (insanityMeter >= insaneToSillhouette)
        {
            timeForSillhouette -= Time.deltaTime;

            if (timeForSillhouette <= 0)
            {
                var sillhouette = Instantiate(sillhouettePrefab);
                sillhouette.transform.position = spawnSillhouetteZoneCenter + new Vector3((Random.value * 2 - 1) * 0.5f * spawnSillhouetteZoneSize.x, 0, (Random.value * 2 - 1) * 0.5f * spawnSillhouetteZoneSize.y);

                timeForSillhouette = Random.Range(sillhouetteTimePeriod.x, sillhouetteTimePeriod.y);
            }
        }

        if (insanityMeter >= insaneToShake)
        {
            timeForShake -= Time.deltaTime;

            if (timeForShake <= 0)
            {
                bool directional = Random.value <= chanceToDirectionalShake;

                cameraShaker.shakingTime = Random.Range(shakeTime.x, shakeTime.y);
                cameraShaker.intensity = Random.Range(shakeIntensity.x, shakeIntensity.y);

                if (directional)
                {
                    cameraShaker.direction = Random.insideUnitSphere;
                }
                else
                {
                    cameraShaker.direction = Vector3.zero;
                }

                timeForShake = Random.Range(shakePeriod.x, shakePeriod.y);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnSillhouetteZoneCenter, new Vector3(spawnSillhouetteZoneSize.x, 0, spawnSillhouetteZoneSize.y));
    }
}
