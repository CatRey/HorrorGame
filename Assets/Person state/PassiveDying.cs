using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassiveDying : MonoBehaviour
{
    public IsGrounded isDrowning;
    public BasicBreakable3D isSuffocating;

    public float timeToSuffocate, timeToFeel, returnAirSpeed;
    float timeSuffocating;
    public AudioSource suffocationSound;
    public float volumeSetSpeed;

    public VingnetteGenerator drowning;
    public float normalMultiplier, drownedMultiplier;
    public float V, H;

    public int mainMenuScene;

    private void Start()
    {
        drowning.enabled = false;
    }


    private void Update()
    {
        if (isDrowning.isGrounded || isSuffocating.broken)
        {
            timeSuffocating = Mathf.Max(0, timeSuffocating + Time.deltaTime);

            drowning.enabled = timeSuffocating >= timeToFeel;
            drowning.image.enabled = timeSuffocating >= timeToFeel;
            if (timeSuffocating >= timeToFeel)
            {
                drowning.multiplier = Mathf.Lerp(normalMultiplier, drownedMultiplier, (timeSuffocating - timeToFeel) / timeToSuffocate);
                drowning.multiplier += H;
                drowning.multiplier *= drowning.multiplier;
                drowning.multiplier = V / drowning.multiplier;
                drowning.multiplier -= V / H / H;

                if ((timeSuffocating - timeToFeel) >= timeToSuffocate)
                {
                    OnDeathVignette.Died = true;
                    SceneManager.LoadScene(mainMenuScene);
                }
                if ((timeSuffocating - timeToFeel) >= (timeToSuffocate - suffocationSound.clip.length))
                {
                    if (!suffocationSound.isPlaying)
                    {
                        suffocationSound.Play();
                        suffocationSound.volume = 0;
                    }
                    suffocationSound.volume = Mathf.Lerp(suffocationSound.volume, 1, volumeSetSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            suffocationSound.Stop();

            drowning.enabled = timeSuffocating >= timeToFeel;
            drowning.image.enabled = timeSuffocating >= timeToFeel;

            timeSuffocating -= Time.deltaTime * returnAirSpeed;
            drowning.multiplier = Mathf.Lerp(normalMultiplier, drownedMultiplier, (timeSuffocating - timeToFeel) / timeToSuffocate);
            drowning.multiplier += H;
            drowning.multiplier *= drowning.multiplier;
            drowning.multiplier = V / drowning.multiplier;
            drowning.multiplier -= V / H / H;
        }
    }
}
