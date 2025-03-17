using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnabler : MonoBehaviour
{
    public GameObject wingsuit;
    
    void Start()
    {
        wingsuit.SetActive(true);
    }

    
}
