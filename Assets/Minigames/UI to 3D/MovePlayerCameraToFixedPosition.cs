using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerCameraToFixedPosition : MonoBehaviour
{
    public Transform copyTransforms;
    public float moveTime;
    Vector3 startPosition, startForward;
    public MoveState moveState;

    private void Start()
    {
        
    }


    private void Update()
    {
        switch (moveState)
        {
            case MoveState.none:
                break;
            case MoveState.toLocation:
                Camera.main.transform.position = Vector3.Lerp(startPosition, copyTransforms.position, Time.deltaTime / moveTime);
                Camera.main.transform.forward = Vector3.Lerp(startForward, copyTransforms.forward, Time.deltaTime / moveTime);
                break;
            case MoveState.restoring:
                Camera.main.transform.position = Vector3.Lerp(copyTransforms.position, startPosition, Time.deltaTime / moveTime);
                Camera.main.transform.forward = Vector3.Lerp(copyTransforms.forward, startForward, Time.deltaTime / moveTime);
                break;
            default:
                break;
        }
    }

    public void MoveToPosition()
    {
        startPosition = Camera.main.transform.position;
        startForward = Camera.main.transform.forward;
        moveState = MoveState.toLocation;
    }

    public void RestorePosition()
    {

        moveState = MoveState.restoring;
    }

    public enum MoveState
    {
        none,
        toLocation,
        restoring,
    }
}
