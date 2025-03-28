using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TankDrone : MonoBehaviour
{
    // 0 - idle, 1 - wandering, 2 - moving to target, 3 - attacking

    public GameObject tankDrone;
    public int state;
    public bool atPosition;
    Vector3 targetPosition;
    private float speed;
    public bool hacked;

    void Start()
    {
        atPosition = true;
        speed = 1f;
        state = 1;
        hacked = false;
    }


    void Update()
    {
        if (state == 0)
            Idle();
        else if (state == 1)
            Wandering();
        else if (state == 2)
            MoveToTarget();
        else if (state == 3)
            AttackTarget();

        AvoidObstacles();

    }

    public void Idle()
    {
        // idle
    }

    public void SwitchToAttack()
    {

    }

    public void Wandering()
    {
        if (atPosition == true)
        {
            NewPosition();
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.LookAt(targetPosition);

        if (Mathf.Abs(transform.position.x - targetPosition.x) <= 0.5f && Mathf.Abs(transform.position.z - targetPosition.z) <= 0.5f) // within 0.5 of target on x and z axis
        {
            atPosition = true;
        }
    }

    public void NewPosition()
    {
        float targetX = tankDrone.transform.position.x + Random.Range(-5f, 6f); // current X position +- 5
        float targetZ = tankDrone.transform.position.z + Random.Range(-5f, 6f); // current Z position +- 5

        targetPosition = new Vector3(targetX, tankDrone.transform.position.y, targetZ);

        atPosition = false;
    }

    public void MoveToTarget()
    {

    }

    public void AttackTarget()
    {

    }

    public void AvoidObstacles()
    {



    }
}
