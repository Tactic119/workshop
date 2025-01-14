using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    private PlayerMovement pm;
    private Rigidbody rb;


    [Header("Flying")]
    public bool hovering;
    public bool soaring;
    public bool airBorne;
    public float startUpBoost;
    public float soarBuffer;
    public float soarSpeed;
    public float hoverSpeed;


    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(airBorne == true)
        {
            MyInput();
        }

        if(soarBuffer > 0)
            soarBuffer -= Time.deltaTime;
    }

    public void TakeOff()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        //rb.AddForce(Vector3.up * 1000f, ForceMode.Force);
    }

    private void MyInput()
    {
        if (hovering == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector3(rb.velocity.x, 8f, rb.velocity.z);
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                rb.velocity = new Vector3(rb.velocity.x, -8f, rb.velocity.z);
            }
            if (Input.GetKey(KeyCode.LeftShift) && soarBuffer <= 0)
            {
                startSoar();
            }
        }
        else if (soaring == true)
        {
            if (Input.GetKey(KeyCode.LeftShift) && soarBuffer <= 0)
            {
                stopSoar();
            }
        }
        if (airBorne == true)
        {
            if (Input.GetKey(KeyCode.F) && pm.startHoverCooldown <= 0)
            {
                disengage();
            }
        }
    }

    public void startSoar() // hovering to soar
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);
        pm.desiredMoveSpeed = soarSpeed;
        hovering = false;
        soaring = true;
        soarBuffer = 0.2f;
        Debug.Log("Start Soar");
    }

    public void stopSoar() // soar to hovering
    {
        pm.desiredMoveSpeed = hoverSpeed;
        hovering = true;
        soaring = false;
        soarBuffer = 0.2f;
        Debug.Log("Stop Soar");

    }

    public void disengage() // stop flying
    {
        pm.desiredMoveSpeed = pm.walkSpeed;
        pm.state = PlayerMovement.MovementState.walking;
        hovering = false;
        soaring = false;
        airBorne = false;
        pm.startHoverCooldown = 0.2f;
        Debug.Log("Disengage");

    }


}
