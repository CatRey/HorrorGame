using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisabler : MonoBehaviour
{
    public UniversalLook playerLookAround;
    public Movement playerMovement;
    public Interactor interactor;

    public static PlayerDisabler playerDisabler;

    private void Start()
    {
        playerDisabler = this;
    }


    private void Update()
    {
        
    }

    public void DisablePlayer()
    {
        playerLookAround.enabled = playerMovement.enabled = interactor.enabled = false;

        playerMovement.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void EnablePlayer()
    {
        playerLookAround.enabled = playerMovement.enabled = interactor.enabled = true;

        playerMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
