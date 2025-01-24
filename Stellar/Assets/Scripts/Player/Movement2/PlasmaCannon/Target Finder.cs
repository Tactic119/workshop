using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;

public class TargetFinder : MonoBehaviour
{
    public GameObject target;
    public Rigidbody rb;
    public Transform ProjectileSpawn;
    private Vector3 direction;
    public GameObject player;
    public PlasmaCannons connons;
    public int speed;
    private bool moving;

    void Start()
    {
        ProjectileSpawn = GameObject.FindGameObjectWithTag("ProjectileSpawn").transform;
        rb = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player");
        connons = player.GetComponent<PlasmaCannons>();

        direction = ProjectileSpawn.transform.forward;

        moving = true;
    }

    
    void Update()
    {
        if(moving) rb.AddForce(direction.normalized * speed, ForceMode.Force);
    }

    public void OnTriggerEnter(Collider other)
    {
        moving = false;

        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("hit");


        }
        else 
        { 
            //Debug.Log("miss"); 
        }

        speed = 0;

        Instantiate(target, transform.position, Quaternion.identity);

        connons.target = target;

        Destroy(gameObject);

    }
}
