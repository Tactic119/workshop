using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;

public class TargetFinder : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public Transform ProjectileSpawn;
    private Vector3 direction;
    public GameObject player;
    public PlasmaCannons connons;

    void Start()
    {
        ProjectileSpawn = GameObject.FindGameObjectWithTag("ProjectileSpawn").transform;
        rb = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player");
        connons = player.GetComponent<PlasmaCannons>();

        direction = ProjectileSpawn.transform.forward;
    }

    
    void Update()
    {
        rb.AddForce(direction.normalized * 500f, ForceMode.Force);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");


        }
        else { Debug.Log("miss");  }

        target = GetComponent<Transform>();
        connons.target = target;
        Destroy(gameObject);

    }
}
