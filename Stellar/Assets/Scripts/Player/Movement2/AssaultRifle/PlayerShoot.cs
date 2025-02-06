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

    }
}
