using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Katana : MonoBehaviour
{

    public bool drawn;
    public int State;
    public int newState;
    public bool settingState;
    public float settingStateTimer;
    public Animator anim;
    public float swingTimer;
    public float comboTimer;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        drawn = false;
        State = 4;
        // idle = 0, Swing1 = 1, Swing = 2, sheath = 3, sheathed = 4, draw = 5
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(drawn) // sheath
            {
                drawn = false;
                State = 3;
                anim.SetInteger("State", State);
                anim.SetBool("Drawn", drawn);
            }
            else if(!drawn) // draw
            {
                drawn = true;
                State = 5;
                anim.SetInteger("State", State);
                anim.SetBool("Drawn", drawn);
            }
        }
        if(Input.GetMouseButton(0) && drawn && swingTimer <= 0)
        {
            if(comboTimer <= 0) // Swing1
            {
                State = 1;
                swingTimer = 0.25f;
                comboTimer = 2.5f;
                SetStatePeriod(0, 0.34f);
                anim.SetInteger("State", State);
                anim.SetBool("Drawn", drawn);
            }
            else // Swing2
            {
                State = 2;
                swingTimer = 0.3f;
                comboTimer = 0f;
                SetStatePeriod(0, 0.25f);
                anim.SetInteger("State", State);
                anim.SetBool("Drawn", drawn);
            }
            
        }

        if(swingTimer > 0)
            swingTimer -= Time.deltaTime;
        if(comboTimer > 0)
            comboTimer -= Time.deltaTime;
        if(settingStateTimer > 0)
            settingStateTimer -= Time.deltaTime;

        if (settingState == true && settingStateTimer <= 0)
        {
            settingState = false;
            State = newState;
            anim.SetInteger("State", State);
            anim.SetBool("Drawn", drawn);
        }

        
    }

    public void SetStatePeriod(int state, float timer)
    {
        newState = state;
        settingStateTimer = timer;
        settingState = true;
    }

}
