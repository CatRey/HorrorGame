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

        minigameObject = Instantiate(minigamePrefab, canvas);
        minigameObject.GetComponent<Minigame>().invoker = this;
    }

    public void MinigameStopped()
    {
        playerDisabler.EnablePlayer();

        Destroy(minigameObject);

        enabled = false;
    }
}
