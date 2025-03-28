using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject playerUI;
    public GameObject playerUI2;

    private void Start()
    {
        playerUI.SetActive(true);
        //playerUI2.SetActive(true);
    }
}
