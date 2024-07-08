using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingThroughHole : MonoBehaviour
{
    public float maxAngle;

    public Transform player;
    public Vector3 toPosition;
    public float lowerToHeight;
    public float movingTime;
    public static bool Moving;
    
    private void Start()
    {

    }


    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Moving) return;

        var angle = Vector3.Angle(toPosition - player.position, Camera.main.transform.forward);
        if (angle > maxAngle) return;

        StartCoroutine(MovePlayer(player.position));
    }

    public IEnumerator MovePlayer(Vector3 startPosition)
    {
        Moving = true;
        float timeMoving = 0;

        while (timeMoving < movingTime)
        {
            var target = toPosition;
            var totalTime = movingTime / 2;
            var offsetTime = 0f;
            var from = startPosition;
            if (timeMoving <= movingTime / 2)
            {
                target = (toPosition + startPosition) / 2;
                target.y = lowerToHeight;
            }
            else
            {
                offsetTime = movingTime / 2;

                from = (toPosition + startPosition) / 2;
                from.y = lowerToHeight;
            }

            player.position = Vector3.Lerp(from, target, ((timeMoving += Time.fixedDeltaTime) - offsetTime) / totalTime);

            yield return new WaitForFixedUpdate();
        }

        Moving = false;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(toPosition, 0.01f);
    }
}
