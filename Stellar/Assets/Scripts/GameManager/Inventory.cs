using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    KeyCode inventoryKey = KeyCode.Tab;
    public bool inventoryOpen;
    public GameObject inventoryScreen;
    float openTimer;

    [Header("Stat Values")]
    public int attack;
    public int deffense;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        OpenClose();

        PlayerInteraction();

        if(openTimer > 0)
            openTimer -= Time.deltaTime;
    }

    private void PlayerInteraction()
    {
        if(inventoryOpen)
        {

        }
    }

    private void OpenClose()
    {
        if (Input.GetKey(inventoryKey) && openTimer <= 0)
        {
            if (inventoryOpen == true) // close inventory
            {
                inventoryOpen = false;
                inventoryScreen.SetActive(inventoryOpen);
            }
            else // open inventory
            {
                inventoryOpen = true;
                inventoryScreen.SetActive(inventoryOpen);
            }

            openTimer = 0.25f;
        }
    }
}
