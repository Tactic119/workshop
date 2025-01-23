using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaCannons : MonoBehaviour
{
    public Transform ProjStart;
    public float launchTimer;
    public bool readyToFire;
    public GameObject targetFinder;
    public GameObject target;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            FindTarget();
            readyToFire = true;
            launchTimer = 0.5f;
        }

        if(launchTimer > 0)
            launchTimer -= Time.deltaTime;
    }

    public void FindTarget()
    {
        Instantiate(targetFinder);

        
    }

    public void LaunchMissile()
    {

    }
}
