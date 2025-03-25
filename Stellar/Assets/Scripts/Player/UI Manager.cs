using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject playerUI;

    private void Start()
    {
        playerUI.SetActive(true);
    }
}
