using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Mac10 : MonoBehaviour
{
    public bool Shooting;
    public int State;
    public float aimCounter;
    public float reloadTimer;
    public float shootTimer;
    public bool active;

    public Gun gunScript;
    Animator anim;

    void Start()
    {
        Shooting = false;
        State = 0;

        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(1)) // aim
        {
            State = 1;
        }
        if(Input.GetMouseButtonUp(1)) // hipfire
        {
            State = 0;
        }
        if (Input.GetKey(KeyCode.R) && gunScript.ammo < gunScript.maxAmmo && reloadTimer <= 0)
        {
            reload();
        }

        if(reloadTimer > 0)
            reloadTimer -= Time.deltaTime;
        if(shootTimer > 0)
            shootTimer -= Time.deltaTime;

        if (reloadTimer <= 0)
            anim.SetBool("Reloading", false);
        if (shootTimer <= 0)
            anim.SetBool("Shooting", false);
        anim.SetInteger("State", State);
        anim.SetBool("Shooting", Shooting);
    }

    public void reload()
    {
        reloadTimer = 2.5f;
        gunScript.ammo = gunScript.maxAmmo;
        anim.SetBool("Reloading", true);
    }

    public void ShootAnim()
    {
        anim.SetBool("Shooting", true);
        shootTimer = 0.0005f;
    }
}
