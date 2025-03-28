using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TankDrone : MonoBehaviour
{
    // 0 - idle, 1 - wandering, 2 - moving to target, 3 - attacking

    public GameObject tankDrone;
    public int state;
    public bool atPosition;
    private Vector3 targetPosition;
    private Vector3 enemyPosition;
    public float speed;
    public bool hacked;

    public float sightDistance;
    public float fieldOfView;
    public GameObject Player;
    public bool foundTarget;
    public float agroTimer;
    public float attackTimer;
    public float attackDistance;
    public float gunAngle;
    public GameObject gunRotation;

    void Start()
    {
        atPosition = true;
        speed = 1.3f;
        state = 1;
        hacked = false;
        foundTarget = false;
        agroTimer = 0f;
        attackTimer = 0f;

        sightDistance = 7f;
        fieldOfView = 60f;
        attackDistance = 3.5f;

        gunAngle = 0f;
        //gunRotation.transform.rotation = new Vector3(gunRotation.transform.rotation.x, gunRotation.transform.rotation.y, gunAngle);
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

        if(state >= 1)Search();

        if (agroTimer > 0)
            agroTimer -= Time.deltaTime;
        else if (agroTimer <= 0)
        {
            state = 1;
            foundTarget = false;
        }
    }

    public void Search()
    {
        if (hacked == true)enemyPosition = new Vector3(0, 0, 0);
        else if (hacked == false)
            enemyPosition = Player.transform.position;

        if (Vector3.Distance(transform.position, enemyPosition) < sightDistance)
        {
            Vector3 targetDirection = enemyPosition - transform.position;
            float angleToTarget = Vector3.Angle(targetDirection, transform.forward);
            if (angleToTarget >= -fieldOfView && angleToTarget <= fieldOfView)
            {
                Ray ray = new Ray(transform.position, targetDirection);
                Debug.DrawRay(ray.origin, ray.direction * sightDistance);

                foundTarget = true;
                agroTimer = 5f;
                if (state == 1)
                {
                    state = 2;
                }
            }
        }
    }

    public void Idle()
    {
        // idle
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
        speed = 1.8f;
        FindEnemyPosition();

        if (Vector3.Distance(transform.position, enemyPosition) > attackDistance) transform.position = Vector3.MoveTowards(transform.position, enemyPosition, speed * Time.deltaTime);
        else if (Vector3.Distance(transform.position, enemyPosition) <= attackDistance) state = 3;

        transform.LookAt(enemyPosition);
    }

    public void FindEnemyPosition()
    {
        enemyPosition = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
    }
       

    public void AttackTarget()
    {

    }

    public void AvoidObstacles()
    {



    }
}
