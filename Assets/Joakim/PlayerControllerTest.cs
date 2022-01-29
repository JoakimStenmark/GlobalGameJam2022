using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    CharacterController character;
    Vector3 velocity;
    [SerializeField] private float power = 10;
    [SerializeField] private float drag = 0.9f;

    [SerializeField] private float maxDeltaLength = 10;

    [SerializeField] private Vector3 gravity;

    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {

        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), 0, 0);
        mouseDelta = Vector3.ClampMagnitude(mouseDelta, maxDeltaLength);
        
        if (mouseDelta.sqrMagnitude > 0)
        {
            velocity += mouseDelta * power * Time.deltaTime;
        }

        velocity *= drag;

        character.Move(velocity + gravity);

    }


}
