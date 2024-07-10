using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{
    public KeyCode openClose;
    public GameObject panel;
    public PlayerDisabler disabler;

    private void Start()
    {
        
    }


    private void Update()
    {
        if (Input.GetKeyDown(openClose))
        {
            panel.SetActive(!panel.activeSelf);

            if (panel.activeSelf)
            {
                disabler.DisablePlayer();
                Cursor.visible = true;
            }
            else
            {
                disabler.EnablePlayer();
                Cursor.visible = false;
            }
        }
    }
}
