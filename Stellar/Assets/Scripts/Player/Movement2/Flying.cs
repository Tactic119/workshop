using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{

    [Header("Flying")]
    public bool hovering;
    public bool soaring;
    public bool airBorne;
    public float startUpBoost;


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

    public void TakeOff()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        //rb.AddForce(Vector3.up * 1000f, ForceMode.Force);

        Debug.Log("take off function ran");
    }

    private void MyInput()
    {

    }

    public void startSoar() // hovering to soar
    {

    }

    public void stopSoar() // soar to hovering
    {

    }

    public void disengage() // stop flying
    {

    }

    
}
