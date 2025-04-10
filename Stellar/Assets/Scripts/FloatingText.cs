using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    Transform mainCam;
    Transform unit;
    Transform worldSpaceCanvas;

    public Vector3 offset;

    void Start()
    {
        offset = new Vector3 (0f, 0.35f, 0f);

        mainCam = Camera.main.transform;
        unit = transform.parent;
        worldSpaceCanvas = GameObject.FindObjectOfType<Canvas>().transform;

        //transform.SetParent(worldSpaceCanvas);
    }
    
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position); // look at the camera
        transform.position = unit.position + offset;

    }
}
