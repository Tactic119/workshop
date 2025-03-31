using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
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

    public void PlayClipOnce(AudioClip clip1, AudioSource source1)
    {
        source1.PlayOneShot(clip1);
    }

    public void PlayClipLoop(AudioClip clip1, AudioSource source1, float clipLength)
    {

    }
}
