using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private Gun Gun;

    public void OnShoot()
    {
        Gun.Shoot();
    }
}
