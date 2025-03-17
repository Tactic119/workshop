using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    private EnemyHealth enemyHealth;
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

        if(other.gameObject.tag == "Bullet")
        {
            damage = 10;
        }
        else { damage = 0; }

        enemyHealth.TakeDamage(damage, part);
    }
}
