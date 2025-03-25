using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class miniCube : MonoBehaviour
{
    Rigidbody rb;
    float lifeSpan;
    float maxAngle;
    int forcePower;
    
    void Start()
    {
        lifeSpan = 1f;
        maxAngle = 360f;
        forcePower = 5;
        rb = GetComponent<Rigidbody>();

        Vector3 direction = new Vector3(Random.Range(-1 * maxAngle, maxAngle), Random.Range(-1 * maxAngle, maxAngle), Random.Range(-1 * maxAngle, maxAngle)).normalized;
        rb.AddForce(direction * forcePower, ForceMode.Impulse);
    }

    
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
            Destroy(gameObject);
    }
}
