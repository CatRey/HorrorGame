
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject audioPrefab;
    private static GameObject audioPlayer;

    public void Start()
    {
        audioPlayer = audioPrefab;
    }

    static public void Play(AudioClip shotSound)
    {
        GameObject go = GameObject.Instantiate(audioPlayer) as GameObject;

        Audio a = go.GetComponent<Audio>();
        a.PlaySoundOnce(shotSound);
    }
    static public void Play(AudioClip shotSound, float volume = 1)
    {
        GameObject go = GameObject.Instantiate(audioPlayer) as GameObject;
        var aud = go.GetComponent<AudioSource>();
        aud.volume = volume;
        Audio a = go.GetComponent<Audio>();
        a.PlaySoundOnce(shotSound);
    }

    static public void Play(AudioClip shotSound, Vector3 position, float volume = 1)
    {
        GameObject go = GameObject.Instantiate(audioPlayer, position, audioPlayer.transform.rotation) as GameObject;
        var aud = go.GetComponent<AudioSource>();
        aud.spatialBlend = 1;
        aud.volume = volume;
        go.GetComponent<Audio>().PlaySoundOnce(shotSound);
    }
    static public void Play(AudioClip shotSound, Vector3 position, Transform parent, float volume = 1)
    {
        GameObject go = GameObject.Instantiate(audioPlayer, position, audioPlayer.transform.rotation, parent) as GameObject;
        var aud = go.GetComponent<AudioSource>();
        aud.spatialBlend = 1;
        aud.volume = volume;
        go.GetComponent<Audio>().PlaySoundOnce(shotSound);
    }
}