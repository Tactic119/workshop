using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Breakable : MonoBehaviour
{
    public int health;
    public float hitCoolDown;
    public GameObject childCube;
    
    void Start()
    {
        health = 3;
    }

    
    void Update()
    {
        if(hitCoolDown > 0)
            hitCoolDown -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" && hitCoolDown <= 0)
        {
            health -= 1;
            hitCoolDown = 0.01f;
            hitEffects();

            if (health <= 0)
            {
                breakEffects();
                Destroy(gameObject);
            }
        }
    }

    private void hitEffects()
    {
        Vector3 position1 = new Vector3(gameObject.transform.position.x + Random.Range(-0.5f, 0.5f), gameObject.transform.position.y + Random.Range(-0.5f, 0.5f), gameObject.transform.position.z + Random.Range(-0.5f, 0.5f));
        Instantiate(childCube, position1, Quaternion.identity);
    }

    private void breakEffects()
    {
        int amount = Random.Range(3, 4);

        Vector3 position1 = new Vector3(gameObject.transform.position.x + Random.Range(-0.5f, 0.5f), gameObject.transform.position.y + Random.Range(-0.5f, 0.5f), gameObject.transform.position.z + Random.Range(-0.5f, 0.5f));
        Instantiate(childCube, position1, Quaternion.identity);

        Vector3 position2 = new Vector3(gameObject.transform.position.x + Random.Range(-0.5f, 0.5f), gameObject.transform.position.y + Random.Range(-0.5f, 0.5f), gameObject.transform.position.z + Random.Range(-0.5f, 0.5f));
        Instantiate(childCube, position2, Quaternion.identity);

        Vector3 position3 = new Vector3(gameObject.transform.position.x + Random.Range(-0.5f, 0.5f), gameObject.transform.position.y + Random.Range(-0.5f, 0.5f), gameObject.transform.position.z + Random.Range(-0.5f, 0.5f));
        Instantiate(childCube, position3, Quaternion.identity);

        Vector3 position4 = new Vector3(gameObject.transform.position.x + Random.Range(-0.5f, 0.5f), gameObject.transform.position.y + Random.Range(-0.5f, 0.5f), gameObject.transform.position.z + Random.Range(-0.5f, 0.5f));
        Instantiate(childCube, position4, Quaternion.identity);
    }
}
