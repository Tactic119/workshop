using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Gun ar;
    [SerializeField]
    private Gun cannon;
    [SerializeField]
    private Gun mac10;
    public Mac10 macScript;
    public GameObject macObj;


    public void Update()
    {
        
        if(Input.GetMouseButton(0) && ar != null)
        {
            ar.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Q) && cannon != null)
        {
            cannon.Shoot();
        }
        if(Input.GetMouseButton(0) && mac10 != null && macScript.reloadTimer <= 0 && macObj.activeInHierarchy)
        {
            if(macScript.gunScript.ammo > 0)
            {
                mac10.Shoot();
                macScript.ShootAnim();
            }
            else if(macScript.gunScript.ammo <= 0)
            {
                macScript.reload();
                macScript.State = 0;
            }
        }
        if (macScript.State == 0)
        {
            mac10.AddBulletSpread = true;
        }
        else { mac10.AddBulletSpread = false; }

    }

}
