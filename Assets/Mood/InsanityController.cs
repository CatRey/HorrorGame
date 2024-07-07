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
    public Transform[] spawnSillhouettePositions;
    GameObject sillhouetteNow;
    public Transform player;
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
            if (timeForSillhouette <= 0 && !sillhouetteNow)
            {
                timeForSillhouette = Random.Range(sillhouetteTimePeriod.x, sillhouetteTimePeriod.y);
            }

            timeForSillhouette -= Time.deltaTime;


            if (timeForSillhouette <= 0 && !sillhouetteNow)
            {
                var sillhouette = Instantiate(sillhouettePrefab);
                sillhouette.transform.position = spawnSillhouettePositions[Random.Range(0, spawnSillhouettePositions.Length)].position;
                var maxDist = sillhouette.GetComponent<HiddenSilhouette>().maxAngle;
                while (Vector3.Distance(player.position, sillhouette.transform.position) <= maxDist)
                {
                    sillhouette.transform.position = spawnSillhouettePositions[Random.Range(0, spawnSillhouettePositions.Length)].position;
                }
                sillhouetteNow = sillhouette;
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

    public void AddInssanity(float amount)
    {
        insanityMeter += amount;
    }
    public void SetInssanity(float amount)
    {
        insanityMeter = amount;
    }
}
