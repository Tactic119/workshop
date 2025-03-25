using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : MonoBehaviour
{
    public float size;
    public float startSize;
    public float endSize;
    public float expandSpeed;

    void Start()
    {
        size = startSize;
    }

    void Update()
    {
        gameObject.transform.localScale = new Vector3(size, size, size);

        if (size < endSize) 
            size += (Time.deltaTime * expandSpeed);
    }
}
