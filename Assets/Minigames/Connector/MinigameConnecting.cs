using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MinigameConnecting : Minigame
{
    public GameObject connectablePrefab;
    public Vector2Int connectableAmount;
    public List<Image> connecting = new ();
    public Color connectedColor = Color.green, disconnectedColor = Color.white;
    public List<int> connected = new ();
    public GameObject lineSegmentPrefab;
    public List<GameObject> lineSegments = new ();
    public float lineWidth = 0.1f, lineHeightModifier = 0.5f;
    Vector3 startingMousePosition;
    
    private void Start()
    {
        int connectableCount = Random.Range(connectableAmount.x, connectableAmount.y);

        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        for (int i = 0; i < connectableCount; i++)
        {
            var connectable = Instantiate(connectablePrefab, transform.GetChild(1));
            connectable.transform.localPosition = new Vector3(Random.Range(-screenSize.x, screenSize.x), Random.Range(-screenSize.y, screenSize.y)) / 2;
            connecting.Add(connectable.GetComponent<Image>());
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startingMousePosition = Input.mousePosition;
            var newLine = Instantiate(lineSegmentPrefab, transform.GetChild(0));
            lineSegments.Add(newLine);
        }

        if (Input.GetMouseButton(0))
        {

            if (connected.Count > 0)
            {
                var vectorBetween = Input.mousePosition - connecting[connected[^1]].transform.position;
                lineSegments[^1].transform.position = (connecting[connected[^1]].transform.position + Input.mousePosition) / 2;
                lineSegments[^1].transform.up = vectorBetween;
                lineSegments[^1].transform.localScale = new Vector3(lineWidth, vectorBetween.magnitude * lineHeightModifier);
            }
            else
            {
                var vectorBetween = Input.mousePosition - startingMousePosition;
                lineSegments[^1].transform.position = (startingMousePosition + Input.mousePosition) / 2;
                lineSegments[^1].transform.up = vectorBetween;
                lineSegments[^1].transform.localScale = new Vector3(lineWidth, vectorBetween.magnitude * lineHeightModifier);
            }


            for (int i = 0; i < connecting.Count; i++)
            {
                if (connected.Contains(i)) continue;

                var connectable = connecting[i];
                if (connectable.rectTransform.rect.Contains(connectable.transform.InverseTransformPoint(Input.mousePosition)))
                {
                    connecting[i].color = connectedColor;

                    if (connected.Count > 0)
                    {
                        var vectorBetween = connecting[connected[^1]].transform.position - connecting[i].transform.position;
                        lineSegments[^1].transform.position = (connecting[i].transform.position + connecting[connected[^1]].transform.position) / 2;
                        lineSegments[^1].transform.up = vectorBetween;
                        lineSegments[^1].transform.localScale = new Vector3(lineWidth, vectorBetween.magnitude * lineHeightModifier);
                    }

                    connected.Add(i);

                    var newLine = Instantiate(lineSegmentPrefab, transform.GetChild(0));
                    lineSegments.Add(newLine);
                }
            }

            if (connected.Count >= connecting.Count)
            {
                invoker.MinigameStopped();
            }


        }
        else
        {
            foreach (var index in connected)
            {
                connecting[index].color = disconnectedColor;
            }

            foreach (var line in lineSegments)
            {
                Destroy(line);
            }

            lineSegments.Clear();
            connected.Clear();
        }
    }
}
