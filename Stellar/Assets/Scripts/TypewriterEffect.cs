using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshPro uiText;
    public float delay = 0.1f;
    private string fullText;

    void Start()
    {
        fullText = uiText.text;
        uiText.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in fullText)
        {
            uiText.text += letter;
            yield return new WaitForSeconds(delay);
        }
    }
}