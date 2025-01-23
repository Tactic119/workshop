using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    public GameObject target;
    public GameObject ProjectileSpawn;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = ProjectileSpawn.transform.position;
        transform.rotation = ProjectileSpawn.transform.rotation;
    }

    
    void Update()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.x, 5f);
    }
}
