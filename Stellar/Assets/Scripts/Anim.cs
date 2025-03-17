using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{

    // 0 = idleDown,1 = SwingUpControls, 2 = ControlSpin, 3 = SwingDownControls, 4 = IdleUp 
    public int state;

    // 0 = no direction, 1 = forward, 2 = right, 3 = left, 4 = backward
    public int direction;

    public bool hovering;

    public GameObject playerModel;
    Animator pAnim;


    // Start is called before the first frame update
    void Start()
    {
        state = 0;

        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        pAnim = playerModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Animator>().SetInteger("State", state);
        GetComponent<Animator>().SetInteger("Direction", direction);

        if (state == 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                direction = 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                direction = 2;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = 3;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                direction = 4;
            }
            if (!(Input.GetKey(KeyCode.W)) && !(Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.S)))
            {
                direction = 0;
            }
        }

    }

    public void TakeOffAnim()
    {
        state = 1;
    }

    public void DisengageAnim()
    {
        state = 3;
    }

    public void SetSoarState(int x)
    {
        pAnim.SetInteger("State", x);

    }

    
}
