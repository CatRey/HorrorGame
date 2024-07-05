using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connector3D : InteractableInCollider
{
    public Plane plane;
    public GameObject connectablePrefab;
    public Vector2Int connectableAmount;
    public List<Overlayable3D> connecting = new();
    public Color connectedColor = Color.green, disconnectedColor = Color.white;
    public List<int> connected = new();
    public GameObject lineSegmentPrefab;
    public List<GameObject> lineSegments = new();
    public float lineWidth = 0.1f, lineHeightModifier = 0.5f;
    Vector3 startingMousePosition;

    private void Start()
    {
        plane = new Plane(-transform.forward, Vector3.Dot(transform.forward, transform.position));
        base.Start();
    }


    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out float distance);

        if (Input.GetMouseButtonDown(0))
        {
            startingMousePosition = ray.origin + ray.direction * distance;
            lineSegments.Add(Instantiate(lineSegmentPrefab, transform));
        }

        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < connecting.Count; i++)
            {
                if (connected.Contains(i)) continue;

                var node = connecting[i];

                if (node.overlayed)
                {
                    var vector = startingMousePosition - ray.origin - ray.direction * distance;

                    if (connected.Count > 0)
                    {
                        vector = connecting[connected[^1]].transform.position - ray.origin - ray.direction * distance;
                    }

                    lineSegments[^1].transform.position = ray.origin + ray.direction * distance + vector / 2f;
                    lineSegments[^1].transform.up = vector;
                    lineSegments[^1].transform.localScale = new Vector3(lineWidth, vector.magnitude * lineHeightModifier);

                    connected.Add(i);
                    connecting[i].GetComponent<Image>().color = connectedColor;

                    lineSegments.Add(Instantiate(lineSegmentPrefab, transform));


                }
            }

            if (connected.Count >= connecting.Count)
            {
                MakeUninteractable();
                return;
            }



            {
                var vector = startingMousePosition - ray.origin - ray.direction * distance;

                if (connected.Count > 0)
                {
                    vector = connecting[connected[^1]].transform.position - ray.origin - ray.direction * distance;
                }

                lineSegments[^1].transform.position = ray.origin + ray.direction * distance + vector / 2f;
                lineSegments[^1].transform.up = vector;
                lineSegments[^1].transform.localScale = new Vector3(lineWidth, vector.magnitude * lineHeightModifier);
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            foreach (var index in connected)
            {
                connecting[index].GetComponent<Image>().color = disconnectedColor;
            }

            foreach (var line in lineSegments)
            {
                Destroy(line);
            }

            lineSegments.Clear();
            connected.Clear();
        }
    }

    public override void MakeInteractable()
    {
        base.MakeInteractable();


        int connectableCount = Random.Range(connectableAmount.x, connectableAmount.y);
        var rect = GetComponent<RectTransform>().sizeDelta;
        Vector2 screenSize = rect * 0.9f;

        for (int i = 0; i < connectableCount; i++)
        {
            var connectable = Instantiate(connectablePrefab, transform);
            connectable.transform.localPosition = new Vector3(Random.Range(-screenSize.x, screenSize.x), Random.Range(-screenSize.y, screenSize.y)) / 2;
            connecting.Add(connectable.GetComponent<Overlayable3D>());
            connectable.GetComponent<Image>().color = disconnectedColor;
        }
    }

    public override void MakeUninteractable()
    {
        base.MakeUninteractable();

        foreach (var index in connected)
        {
            connecting[index].GetComponent<Image>().color = disconnectedColor;
        }

        foreach (var line in lineSegments)
        {
            Destroy(line);
        }

        foreach (var line in connecting)
        {
            Destroy(line.gameObject);
        }

        lineSegments.Clear();
        connected.Clear();
        connecting.Clear();
    }
}
