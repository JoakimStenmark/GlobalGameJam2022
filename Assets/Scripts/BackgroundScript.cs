using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public float maxDistance = 2000;

    void Update()
    {
        
        if (Camera.main.transform.position.y < transform.position.y - maxDistance)
        {
            transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
        }
    }
}
