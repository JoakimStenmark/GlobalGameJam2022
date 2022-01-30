using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraFollow : MonoBehaviour
{
    public Transform followTarget;
    [SerializeField] float heightOffset = 10;
    [SerializeField] float moveSpeed = 6;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 targetPos = transform.position;
        targetPos.y = followTarget.position.y + heightOffset;

        float dist = (transform.position - targetPos).magnitude* moveSpeed;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, dist * Time.deltaTime);

        //Camera.main.transform.position = new Vector3(transform.position.x, followTarget.position.y + heightOffset, transform.position.z);
        //Camera.main.transform.LookAt(new Vector3(transform.position.x, followTarget.position.y, followTarget.position.z));


    }
}
