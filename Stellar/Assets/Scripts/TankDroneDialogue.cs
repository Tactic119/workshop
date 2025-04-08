using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class TankDroneDialogue : MonoBehaviour
{
    public string text;
    public TMP_Text worldText;
    public float delay = 0.5f;
    private string fullText;
    public float messageDuration;
    public string previousMessage = "iugefouiegouwghvoi";

    void Start()
    {
        text = "";
    }

    
    void Update()
    {
        worldText.text = text;

        if (messageDuration > 0)
            messageDuration -= Time.deltaTime;
        else if (messageDuration <= 0)
            text = "";
    }

    public void StartMessage(string newText = "", float messageDelay = 0.2f)
    {
        if (previousMessage != newText)
        {
            messageDuration = messageDelay * newText.Length;
            delay = messageDelay;
            text = newText;
            fullText = text;
            text = "";
            StartCoroutine(TypeText());
            previousMessage = newText;
        }
    }

    public void TimedMessage(string newText = "", float messageDelay = 0.2f, float timeBeforeStart = 1f)
    {

    }

    IEnumerator TypeText()
    {
        foreach (char letter in fullText)
        {
            text += letter;
            yield return new WaitForSeconds(delay);
        }
    }
    public void ExplodeCountDown()
    {
        
    }
}
