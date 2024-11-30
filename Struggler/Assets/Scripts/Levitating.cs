using UnityEngine;
using System;

public class Levitating : MonoBehaviour
{
    
    public float floatHeight = 0.2f;
    public float floatSpeed = 1f;  

    private Vector3 itemPosition;

    void Start()
    {
        itemPosition = transform.position;

    }

    void Update()
    {
        float newY = itemPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(itemPosition.x, newY, itemPosition.z);
    }
}
