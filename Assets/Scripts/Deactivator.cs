using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    public bool DeactivateBasedOnDistanceToCamera = true;
    public float distanceLimit = 50;

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
