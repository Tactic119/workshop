using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]

public class TypewriterEffect : MonoBehaviour
{
    // Only for prototyping
    [Header("Text String")]
    [SerializeField] private string testText;


    // Basic Typewriter Functionality
    private int _currentVisibileCHaracterIndex;


}
