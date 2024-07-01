using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInteractable : Interactable
{
    public UniversalLook playerLookAround;
    public Movement playerMovement;
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
        playerMovement.enabled = false;
        playerLookAround.enabled = false;

        minigameObject = Instantiate(minigamePrefab, canvas);
    }

    public void MinigameStopped()
    {
        playerMovement.enabled = true;
        playerLookAround.enabled = true;

        Destroy(minigameObject);
    }
}
