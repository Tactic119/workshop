using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaCannons : MonoBehaviour
{
    public Transform ProjStart;
    public float launchTimer;
    public bool readyToFire;
    public GameObject targetFinder;
    public Transform ProjectileSpawn;
    public float shootCoolDown;
    public GameObject target;
    public GameObject plasmaMissile;
    public Transform PMSpawn;


    void Start()
    {
        ProjectileSpawn = GameObject.FindGameObjectWithTag("ProjectileSpawn").transform;
    }

    
    void Update()
    {
        if(Input.GetKey(KeyCode.E) && shootCoolDown <= 0)
        {
            FindTarget();
            readyToFire = true;
            launchTimer = 0.5f;
            shootCoolDown = 5f;
        }

        if(launchTimer > 0)
            launchTimer -= Time.deltaTime;
        if (shootCoolDown > 0)
            shootCoolDown -= Time.deltaTime;

        if(readyToFire && launchTimer <= 0)
        {
            LaunchMissile();
            readyToFire = false;
        }

        //Debug.Log(target);
    }

    public void FindTarget()
    {
        Instantiate(targetFinder, ProjectileSpawn.position, Quaternion.identity);

        
    }

    public void LaunchMissile()
    {
        Instantiate(plasmaMissile, PMSpawn.position, Quaternion.identity);
    }
}
