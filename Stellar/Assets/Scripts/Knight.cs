using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{

    public GameObject hitBox;
    public EnemyHealth enemyHealth;
    public int health;
    
    void Start()
    {
        health = 100;
    }

    
    void Update()
    {
        health = enemyHealth.health;
    }
}
