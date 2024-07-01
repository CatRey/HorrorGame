using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public MinigameInteractable invoker;
    public KeyCode stopGame;
    
    private void Start()
    {
        
    }


    private void Update()
    {
        if (Input.GetKeyDown(stopGame))
        {
            invoker.MinigameStopped();
        }
    }
}
