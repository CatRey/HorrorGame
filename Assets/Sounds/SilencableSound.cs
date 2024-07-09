using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilencableSound : MonoBehaviour
{
    public AudioSource audioSource;

    public float transitionSpeed;
    public AnimationCurve volumeByPower;
    public List<BasicBreakable3D> generators = new();
    public List<BasicBreakable3D> OrGenerators = new();

    private void Start()
    {
        
    }


    private void Update()
    {

        int functional = 0;
        foreach (var item in generators)
        {
            if (!item.broken) functional++;
        }
        int alsoFunctional = 0;
        if (OrGenerators.Count == 0)
        {
            alsoFunctional = functional;
        }
        else
        {
            foreach (var item in OrGenerators)
            {
                if (!item.broken) alsoFunctional++;
            }
        }

        functional = Mathf.Min(functional, alsoFunctional);

        audioSource.volume = Mathf.Lerp(audioSource.volume, volumeByPower.Evaluate(functional), Time.deltaTime * transitionSpeed);
    }
}
