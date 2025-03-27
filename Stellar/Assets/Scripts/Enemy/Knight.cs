using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knight : MonoBehaviour
{

    public EnemyHealth enemyHealth;
    public int health;
    public int leftLegHealth;
    public int rightLegHealth;
    public int headHealth;
    public int rightArmHealth;
    public int leftArmHealth;
    public float speed;
    public Transform playerPos;
    public Vector3 target;
    public float distance;
    public float range;
    public int state = 0;

    public GameObject leftLeg;
    public GameObject rightLeg;
    public GameObject head;
    public GameObject rightArm;
    public GameObject leftArm;

    Animator anim;

    void Start()
    {
        

        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        health = enemyHealth.health;

        if(health > 0)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Destroy(gameObject);
        }


        if (FindDistance() == false)
        {
            state = 0;
        }
        else { state = 1; }
        anim.SetInteger("State", state);

        CheckPartHealth();
    }

    public void MoveTowardsPlayer()
    {
        target = new Vector3(playerPos.position.x, gameObject.transform.position.y, playerPos.position.z);
        
        transform.LookAt(target);

        

        if(FindDistance() == true)transform.position = Vector3.MoveTowards(transform.position, target, speed);
    }

    public bool FindDistance()
    {
        bool inRange;
        float distance = Vector3.Distance(gameObject.transform.position, playerPos.position);
        
        if (distance > range) 
        {
            inRange = false;
        }
        else
        {
            inRange = true;
        }

        return inRange;
    }

    public void CheckPartHealth()
    {
        if (leftArmHealth <= 0)
            Destroy(leftArm);
        if (rightArmHealth <= 0)
            Destroy(rightArm);
        if (headHealth <= 0)
            Destroy(head);
        if (leftLegHealth <= 0)
            Destroy(leftLeg);
        if (rightLegHealth <= 0)
            Destroy(rightLeg);
    }
}
