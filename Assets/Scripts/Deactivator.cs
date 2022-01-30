using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    public float distanceLimit;
    public bool DeactivateBasedOnDistanceToCamera = true;

    private void Update()
    {
        if (DeactivateBasedOnDistanceToCamera)
        {
            float currentDistance = transform.position.y - Camera.main.transform.position.y;

            if (currentDistance > distanceLimit)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
