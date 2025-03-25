using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RoboticArms : MonoBehaviour
{
    public GameObject claw;
    public GameObject rayStart;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Interact();
        }
    }

    public void Interact()
    {
        //GameObject target;

        RaycastHit hit;
        if (Physics.Raycast(rayStart.transform.position, rayStart.transform.forward, out hit))
        {
            //target = hit.GameObject;
        }

    }

    public void PickUp(GameObject item)
    {
        float speed = 1f;
        bool moving = false;

        if (Vector3.Distance(claw.transform.position, item.transform.position) > 0)
            moving = true;

        if (moving)
        {
            claw.transform.position = Vector3.MoveTowards(claw.transform.position, item.transform.position, speed * Time.deltaTime);
            claw.transform.rotation = item.transform.rotation;
        }
    }
}
