using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    public bool ActBasedOnDistanceToCamera = true;
    public bool DestroyOnAct = false;
    public float distanceLimit = 50;

    private void Update()
    {
        if (ActBasedOnDistanceToCamera)
        {
            float currentDistance = transform.position.y - Camera.main.transform.position.y;

            if (currentDistance > distanceLimit)
            {
                if (DestroyOnAct)
                {
                    Destroy(gameObject);
                }
                else
                    gameObject.SetActive(false);

            }
        }
    }
}
