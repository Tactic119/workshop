using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            source.PlayOneShot(clip);
        }
    }
}
