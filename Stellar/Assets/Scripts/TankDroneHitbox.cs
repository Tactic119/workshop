using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDroneHitbox : MonoBehaviour
{
    public TankDrone tankDrone;
    public string part;


    void Start()
    {

    }


    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        int damage;

        if (other.gameObject.tag == "Bullet")
        {
            damage = 25;
        }
        else { damage = 0; }

        tankDrone.TakeDamage(damage, part);
    }
}
