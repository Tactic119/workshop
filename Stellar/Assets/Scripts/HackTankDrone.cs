using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;

public class HackTankDrone : Interactable
{
    public bool hackable;

    void Start()
    {
        hackable = false;
    }

    
    void Update()
    {
        if (hackable)
        {
            //promptMessage = "Hack [E]";
        }
        else
        {
            //promptMessage = "";
        }
    }

    protected override void Interact()
    {
        
    }
}
