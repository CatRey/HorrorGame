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


    protected void Update()
    {
        if (Input.GetKeyDown(stopGame))
        {
            invoker.MinigameStopped();
        }
    }
}
