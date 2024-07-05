
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audio : MonoBehaviour
{
    public void PlaySoundOnce(AudioClip audioClip)
    {
        StartCoroutine(PlaySoundCoroutine(audioClip));
    }

    IEnumerator PlaySoundCoroutine(AudioClip audioClip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        Destroy(gameObject);
    }
}