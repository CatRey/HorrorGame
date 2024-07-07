using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HungerController;

public class Eatable3D : InteractableInCollider
{
    public Overlayable3D isSelected1, isSelected2;
    public GameObject throwPrefab;
    public float throwPower, hungerRestore;
    public float goIntoSpeed;
    bool eating;


    private void Update()
    {
        if (!eating && (isSelected1.overlayed || isSelected2.overlayed))
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Eat(Vector3.Dot(Camera.main.transform.forward, transform.position - Camera.main.transform.position)));
            }
        }
    }

    public IEnumerator Eat(float wasDistance)
    {
        eating = true;
        while (wasDistance > -2)
        {
            transform.position = Camera.main.transform.position + Camera.main.transform.forward * (wasDistance -= goIntoSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        var can = Instantiate(throwPrefab);
        can.transform.position = Camera.main.transform.position;
        can.transform.forward = Random.insideUnitSphere;
        can.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * throwPower;

        ReduceHunger(hungerRestore);

        Destroy(gameObject);
    }
}
