using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    private string part;    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void TakeDamage(int x, string Part)
    {
        health -= x;
        part = Part;
    }
}
