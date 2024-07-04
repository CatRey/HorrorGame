using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoingIntoSubmarine : MonoBehaviour
{
    public float speed, limit;
    public int theGameScene;
    
    private void Start()
    {
        
    }


    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y <= limit)
        {
            SceneManager.LoadScene(theGameScene);
        }
    }
}
