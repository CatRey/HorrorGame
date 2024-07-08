using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperGrabber : MonoBehaviour
{
    public float maxDistance;
    public LayerMask paper;
    public Transform handWithPaper;
    public GameObject handGraphic;

    private void Start()
    {
        
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (handWithPaper.childCount > 0)
            {
                DestroyImmediate(handWithPaper.GetChild(0).gameObject);
            }

            if (Physics.Raycast(new Ray(transform.position, transform.forward), out var hit, maxDistance, paper) && hit.collider.TryGetComponent(typeof(GrabablePaper), out var p))
            {
                GrabablePaper paper = p as GrabablePaper;

                var paperInstance = Instantiate(paper.paperPrefab, handWithPaper).transform;
                paperInstance.localPosition = Vector3.zero;
                paperInstance.localEulerAngles = Vector3.zero;

                Destroy(paper.gameObject);
            }

            handGraphic.gameObject.SetActive(handWithPaper.childCount > 0);
        }
    }
}
