using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TankDroneDialogue : MonoBehaviour
{
    public string text;
    public TextMeshPro worldText;
    public float delay = 0.1f;
    private string fullText;

    public string newMessage;

    void Start()
    {
        text = "";

        newMessage = "ouweg/lewhbvelbvelvhbelvhel.vhelkv";

        StartMessage(newMessage);
    }

    
    void Update()
    {
        worldText.text = text;
    }

    public void StartMessage(string message)
    {
        worldText.text = message;
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
