using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaMissile : MonoBehaviour
{
    public GameObject hitBox;
    public GameObject target;
    public GameObject player;
    public PlasmaCannons connons;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        connons = player.GetComponent<PlasmaCannons>();

        target = GameObject.FindGameObjectWithTag("Target");
    }

    
    void Update()
    {
        if (target != null) 
            moveMissile();

        target = GameObject.FindGameObjectWithTag("Target");
    }

    public void moveMissile()
    {
        transform.LookAt(target.transform.position);

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 25f * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("target destoryed");
        }
        else
        {
            //Debug.Log("complete miss");
        }

        Destroy(gameObject);

    }
}
