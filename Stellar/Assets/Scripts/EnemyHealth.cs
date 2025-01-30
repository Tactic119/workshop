using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void TakeDamage(int x)
    {
        health -= x;
    }
}
