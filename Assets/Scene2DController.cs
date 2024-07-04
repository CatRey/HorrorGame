using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MinigameRotating;
using static System.MathF;

public class Scene2DController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float speed;

    public List<Breakable> breakables = new();
    
    private void Start()
    {
        breakables.AddRange(FindObjectsOfTypeAll(typeof(Breakable)) as Breakable[]);
    }


    private void Update()
    {
        rigidbody.velocity = new Vector2(-Sin(rotation), Cos(rotation)) * speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        int broken = Random.Range(0, breakables.Count);
        int i = 0;
        for (int j = breakables.Count*3; j > 0; j--)
        {
            broken--;

            if (broken == 0 && breakables[i].broken)
            {
                broken++;
            }
            else if (broken == 0)
            {
                breakables[i].Break();
            }

            i++;
            if (i >= breakables.Count)
            {
                i %= breakables.Count;
            }
        }

        // BREAK
    }
}
