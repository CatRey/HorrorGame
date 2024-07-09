using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropable : MonoBehaviour
{
    public AudioClip hit;

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.Play(hit, transform.position);
    }
}
