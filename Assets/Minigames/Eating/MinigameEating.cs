using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameEating : Minigame
{
    public RectTransform spoon;
    public RectTransform canZone, can;
    public GameObject spoonFill;
    public float swapSpeed, swapTime;
    float canStartHeight;
    bool wasInCan, loaded;
    public int spoonsInACan, spoonsLeft, cansLeft;
    public Text cansText;
    bool interactable = true;

    private void Start()
    {
        canStartHeight = can.position.y;
        cansText.text = "Cans left: " + cansLeft.ToString();
    }


    private void Update()
    {
        bool isInCan = canZone.rect.Contains(canZone.InverseTransformPoint(Input.mousePosition));

        if (interactable) spoon.position = new Vector3(spoon.position.x, Mathf.Clamp(Input.mousePosition.y, canZone.position.y + canZone.rect.height/2.5f, canZone.position.y + canZone.rect.height * 2), spoon.position.z);

        if (interactable && !wasInCan && isInCan && !loaded)
        {
            loaded = true;
            spoonFill.SetActive(true);
        }
        if (interactable && wasInCan && !isInCan && loaded)
        {
            loaded = false;
            spoonFill.SetActive(false);
            spoonsLeft--;
            if (spoonsLeft <= 0)
            {
                spoonsLeft = spoonsInACan;
                cansLeft--;
                cansText.text = "Cans left: " + cansLeft.ToString();
                interactable = false;
                StartCoroutine(SwapCans());
            }
        }

        wasInCan = isInCan;

        if (Input.GetKeyDown(stopGame))
        {
            can.position = new Vector3(can.position.x, canStartHeight, can.position.z);
            interactable = true;

            invoker.MinigameStopped();
        }
    }


    public IEnumerator SwapCans()
    {
        interactable = false;
        for (float t = 0; t < swapTime; t+=Time.fixedDeltaTime)
        {

            if (t >= swapTime / 2f)
            {
                //lift
                can.position += Vector3.up * swapSpeed * Time.fixedDeltaTime;
            }
            else
            {
                //swap
                can.position -= Vector3.up * swapSpeed * Time.fixedDeltaTime;
            }

            yield return new WaitForFixedUpdate();
        }
        can.position = new Vector3(can.position.x, canStartHeight, can.position.z);
        interactable = true;
    }
}
