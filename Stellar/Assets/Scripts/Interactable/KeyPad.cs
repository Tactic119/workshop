using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KeyPad : Interactable
{
    public GameObject door;
    private Animator doorAnim;
    private Animator buttonAnim;
    private bool open;
    private bool pressed;
    
    void Start()
    {
        doorAnim = door.GetComponent<Animator>();
        buttonAnim = gameObject.GetComponent<Animator>();

        open = false;
        pressed = false;
    }

    
    void Update()
    {
        if (pressed)
        {
            pressed = false;
            buttonAnim.SetBool("Pressed", pressed);
        }
            
    }

    protected override void Interact()
    {
        pressed = true;
        open = (!open);

        buttonAnim.SetBool("Pressed", pressed);
        doorAnim.SetBool("Open", open);
    }
}
