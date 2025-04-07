using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TankDroneDialogue : MonoBehaviour
{
    public string text;
    public TMP_Text worldText;
    public float delay = 0.2f;
    private string fullText;

    void Start()
    {
        text = "Hello";

        StartMessage("I WIll BRING DEATH UPON YOU");
    }

    
    void Update()
    {
        worldText.text = text;
    }

    public void StartMessage(string newText)
    {
        text = newText;
        fullText = text;
        text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in fullText)
        {
            text += letter;
            yield return new WaitForSeconds(delay);
        }
    }
}
