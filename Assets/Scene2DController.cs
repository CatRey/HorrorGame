using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rotator3D;
using static System.MathF;

public class Scene2DController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Rigidbody player;
    public float speed;
    public float playerPushPower, playerDisableTime;

    public List<Breakable3D> breakables = new();
    
    private void Start()
    {
        breakables.AddRange(FindObjectsOfTypeAll(typeof(Breakable3D)) as Breakable3D[]);
    }


    private void Update()
    {
        rigidbody.velocity = new Vector2(Sin(rotation), Cos(rotation)) * speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        int broken = Random.Range(0, breakables.Count);
        int i = 0;
        for (int j = breakables.Count*3; j > 0; j--)
        {

            if (broken == 0 && breakables[i].broken)
            {
                broken++;
            }
            else if (broken == 0)
            {
                breakables[i].Break();
                StartCoroutine(PushPlayer(breakables[i].transform.position));
                break;
            }

            broken--;

            i++;
            if (i >= breakables.Count)
            {
                i %= breakables.Count;
            }
        }

        // BREAK
    }


    public IEnumerator PushPlayer(Vector3 point)
    {
        PlayerDisabler.playerDisabler.DisablePlayer(false, true);
        player.velocity = (player.transform.position - point + Vector3.up * 2).normalized * playerPushPower;
        float disableTime = playerDisableTime;
         while (disableTime > 0)
        {
            if (!PlayerDisabler.playerDisabler.disabled)
            {
                PlayerDisabler.playerDisabler.DisablePlayer(false, true);
            }
            disableTime -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        PlayerDisabler.playerDisabler.EnablePlayer();
    }
}
