using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArmConnector : MonoBehaviour
{
    public GameObject IKTarget;
    public GameObject head;
    public GameObject tail;
    public GameObject innertail;
    public GameObject follow;

    public float distance;
    public float speed;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Vector3.Distance(IKTarget.transform.position, tail.transform.position) > distance)
        {
            tail.transform.position = Vector3.MoveTowards(tail.transform.position, IKTarget.transform.position, speed * Time.deltaTime);
        }

        follow.transform.position = innertail.transform.position;
        follow.transform.rotation = innertail.transform.rotation;
    }
}
