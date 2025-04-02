using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TankDroneDialogue : MonoBehaviour
{
    public string text;
    public TMP_Text worldText;

    void Start()
    {
        text = "";
    }

    
    void Update()
    {
        worldText.text = text;
    }

    public void SetMessage(string newMessage)
    {
        text = newMessage;

    }
}
