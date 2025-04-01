using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TankDrone : MonoBehaviour
{
    // 0 - idle, 1 - wandering, 2 - moving to target, 3 - attacking

    [Header("Movement")]
    public GameObject tankDrone;
    public int state;
    private bool atPosition;
    private Vector3 targetPosition;
    private Vector3 enemyPosition;
    public float speed;
    public bool hacked;

    [Header("Enemy Detection")]
    private float sightDistance;
    private float fieldOfView;
    public bool foundTarget;
    public float agroTimer;
    private float agroCooldown;

    [Header("Attacking")]
    public GameObject Player;
    public float attackTimer;
    public float attackDistance;
    public float gunAngle;
    public GameObject gunRotation;
    bool canShoot;
    public GameObject gunBarrel;
    private Animator anim;
    private Rigidbody rb;

    [Header("Audio")]
    public AudioPlayer audioPlayer;
    public AudioSource audioSource;
    public AudioClip shootNoise;

    [Header("Health")]
    public GameObject tankGun;
    public GameObject body;
    public GameObject treads;
    public int health;
    public int treadsHealth;
    public GameObject stand1;
    public GameObject stand2;

    private Gun tankGunScript;

    public GameObject hackField;
    private HackTankDrone hack;

    void Start()
    {
        atPosition = true;
        speed = 1.3f;
        state = 1;
        hacked = false;
        foundTarget = false;
        agroTimer = 0f;
        attackTimer = 0.75f;
        canShoot = false;

        sightDistance = 10f;
        fieldOfView = 60f;
        attackDistance = 2.5f;

        health = 500;
        treadsHealth = 300;

        stand1.SetActive(true);
        stand2.SetActive(false);

        gunAngle = 0f;
        gunRotation.transform.localRotation = Quaternion.Euler(90, 0, gunAngle);

        anim = GetComponent<Animator>();
        anim.SetInteger("State", state);

        rb = GetComponent<Rigidbody>();
        tankGunScript = GetComponent<Gun>();
        hack = hackField.GetComponent<HackTankDrone>();
        hack.hackable = false;
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
        else if (agroTimer <= 0 && state != 0)
        {
            state = 1;
            foundTarget = false;
        }

        if(agroCooldown > 0)
            agroCooldown -= Time.deltaTime;
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
                if (state == 1 && agroCooldown <= 0)
                {
                    state = 2;
                    anim.SetInteger("State", state);
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
        else if (Vector3.Distance(transform.position, enemyPosition) <= attackDistance)
        {
            state = 3;
        }

        transform.LookAt(enemyPosition);
    }

    public void FindEnemyPosition()
    {
        enemyPosition = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
    }
       

    public void AttackTarget()
    {
        if (canShoot == false)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0) canShoot = true;
        }
        if (canShoot == true) 
        {
            anim.SetInteger("State", state);
            canShoot = false;
            attackTimer = 0.75f;

            tankGunScript.Shoot();

            audioPlayer.PlayClipOnce(shootNoise, audioSource);

            rb.AddForce(Vector3.up * 15f, ForceMode.Impulse);
            rb.AddForce(-transform.forward * 15f, ForceMode.Impulse);

            atPosition = false;
            foundTarget = false;
            agroCooldown = 1.5f;
            state = 1;
            anim.SetInteger("State", state);
        }
    }

    public void AvoidObstacles()
    {



    }

    public void TakeDamage(int damage, string part)
    {
        if(part == "TankGun" || part == "Body")
        {
            health -= damage;
        }
        else if (part == "Treads")
        {
            treadsHealth -= damage;
            health -= damage;
        }

        if(treadsHealth <= 0)
        {
            state = 0;
            stand1.SetActive(false);
            stand2.SetActive(true);
            hack.hackable = true;
        }

        if (health <= 0)
            Destroy(gameObject);
    }


}
