using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerDisabler;

public class MinigameInteractable : Interactable
{
    public Transform canvas;
    public GameObject minigamePrefab;
    public GameObject minigameObject;
    
    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    public override void Interact()
    {
        playerDisabler.DisablePlayer();

        if (minigameObject)
        {
            minigameObject.SetActive(true);
        }
        else
        {
            minigameObject = Instantiate(minigamePrefab, canvas);
            minigameObject.GetComponent<Minigame>().invoker = this;
        }
    }

    public void MinigameStopped(bool completed = false)
    {
        playerDisabler.EnablePlayer();

        if (completed)
        {
            Destroy(minigameObject);
        }
        else if (minigameObject)
        {
            minigameObject.SetActive(false);
        }

        enabled = !completed;
    }
}
