using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraFollow : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float heightOffset = 10;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, followTarget.position.y + heightOffset, transform.position.z);
        Camera.main.transform.LookAt(new Vector3(transform.position.x, followTarget.position.y, followTarget.position.z));


    }
}
