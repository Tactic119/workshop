using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticArms : MonoBehaviour
{
    public GameObject[] arm = new GameObject[4];
    public Vector3[] offset = new Vector3[4];

    public GameObject rayStart;
    public float armLength;
    public float attackCooldown;

    void Start()
    {
        offset[0] = new Vector3(-10,+10, 0);
        offset[1] = new Vector3(+10,+10, 0);
        offset[2] = new Vector3(-10,-10, 0);
        offset[3] = new Vector3(+10,-10, 0);
    }

    
    void Update()
    {
        CoolDowns();
        PlayerInput();

    }

    public void CoolDowns()
    {
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
    }
    public void PlayerInput()
    {
        if (Input.GetKeyDown("" + 1))
        {
            Attack();
        }
    }


    public void FindTarget()
    {
        int armNumber = Random.Range(0, 3);

        Vector3 target;
        float distance;

        RaycastHit hit;
        if (Physics.Raycast(rayStart.transform.position, rayStart.transform.forward, out hit))
        {
            target = hit.point;
            distance = Vector3.Distance(rayStart.transform.position, target);
            if (distance > armLength) distance = armLength;
            target += offset[armNumber];
        }

    }

    public void Attack()
    {
        int armNumber = Random.Range(0, 3);
        FindTarget();

        //arm[armNumber].
    }


}
