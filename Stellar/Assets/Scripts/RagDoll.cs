using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RagDoll : MonoBehaviour
{
    [SerializeField]
    public bool readyToPush;
    public int maxAngle;
    public int forcePower;
    public float counter;
    public Rigidbody rb;

    void Start()
    {
        readyToPush = true;
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
    }


    void Update()
    {
        if(counter > 0)
            counter -= Time.deltaTime;  

        if(counter <= 0)
        {
            readyToPush = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        rb.useGravity = true;

        if (other.gameObject.tag == "Bullet" && readyToPush)
        {
            Vector3 direction1 = other.gameObject.transform.forward;

            //Vector3 direction2 = new Vector3(Random.Range(-1 * maxAngle, maxAngle), Random.Range(-1 * maxAngle, maxAngle), Random.Range(-1 * maxAngle, maxAngle)).normalized;

            rb.AddForce(direction1 * forcePower, ForceMode.Impulse);

            readyToPush = false;
            counter = 0.01f;
        }

    }
}
