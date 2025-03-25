using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera cam;
    public float distance = 3f;
    public LayerMask mask;
    private PlayerUI playerUI;

    void Start()
    {
        playerUI = GetComponent<PlayerUI>();
    }

    
    void Update()
    {
        playerUI.UpdateText(string.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
