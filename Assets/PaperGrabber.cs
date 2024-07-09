using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperGrabber : MonoBehaviour
{
    public float maxDistance;
    public LayerMask paper;
    public Transform handWithPaper;
    public Animator grabbingHand;
    GameObject grabbedPaper;
    public float canGrab, stale, animationSpeed;
    float time;
    bool couldGrab;
    public Vector3 grabOffset;
    public GameObject handGraphic;

    private void Start()
    {
        
    }


    private void Update()
    {
        Component p = null;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out var hit, maxDistance, paper) && hit.collider.TryGetComponent(typeof(GrabablePaper), out p))
        {
            if (!grabbedPaper)
            {
                grabbingHand.transform.parent.position = hit.point + grabbingHand.transform.parent.TransformVector(grabOffset);
                if (!couldGrab)
                {
                    time = stale;
                }
                else
                {
                    time = Mathf.Lerp(time, canGrab, animationSpeed * Time.deltaTime);
                }
                grabbingHand.SetFloat("play time", time);
            }

            couldGrab = true;
        }
        else
        {
            couldGrab = false;
        }
        grabbingHand.transform.parent.gameObject.SetActive(couldGrab && !grabbedPaper);

        if (Input.GetMouseButtonDown(1))
        {
            if (handWithPaper.childCount > 0)
            {
                DestroyImmediate(handWithPaper.GetChild(0).gameObject);
                grabbedPaper?.SetActive(true);
                grabbedPaper = null;
            }

            if (couldGrab)
            {
                GrabablePaper paper = (GrabablePaper)p;
                grabbedPaper = paper.gameObject;

                var paperInstance = Instantiate(paper.paperPrefab, handWithPaper).transform;
                paperInstance.localPosition = Vector3.zero;
                paperInstance.localEulerAngles = Vector3.zero;

                grabbedPaper.SetActive(false);
            }

            handGraphic.gameObject.SetActive(handWithPaper.childCount > 0);
        }
    }
}
