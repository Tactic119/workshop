using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{

    [Header("Flying")]
    public bool hovering;
    public bool soaring;
    public bool airBorne;


    [Header("References")]
    public Transform orientation;
    private PlayerMovement pm;
    private Rigidbody rb;



    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeOff()
    {
        airBorne = true;
        rb.useGravity = false;
    }

    public void startSoar()
    {

    }

    public void disengage()
    {

    }
}
