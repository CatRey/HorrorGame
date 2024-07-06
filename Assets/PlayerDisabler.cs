using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisabler : MonoBehaviour
{
    public UniversalLook playerLookAround;
    public Movement playerMovement;
    public Interactor interactor;
    public Interactor3D interactor3D;

    public static PlayerDisabler playerDisabler;
    public bool disabled;

    private void Start()
    {
        playerDisabler = this;
    }


    private void Update()
    {
        
    }

    public void DisablePlayer(bool freezePhysics = true, bool closeAllInteractables = false)
    {
        if (closeAllInteractables)
        {
            foreach (MinigameInteractable item in FindObjectsOfTypeAll(typeof(MinigameInteractable)))
            {
                item.MinigameStopped();
            }
        }
        playerLookAround.enabled = playerMovement.enabled = interactor.enabled = interactor3D.enabled = false;

        if (freezePhysics) playerMovement.rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        disabled = true;
    }
    public void EnablePlayer()
    {
        playerLookAround.enabled = playerMovement.enabled = interactor.enabled = interactor3D.enabled = true;

        playerMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        disabled = false;
    }
}
