using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initializer : MonoBehaviour
{
    public Text interactIcon;

    void Start()
    {
        Interactable.interactIcon = interactIcon;
    }


    void Update()
    {
        
    }
}
