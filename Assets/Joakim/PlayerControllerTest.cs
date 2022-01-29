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

    float timeSinceGrounded = 0;

    float gravityScale = 1f;

    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Horisontal Movement Calc

        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), 0, 0);
        mouseDelta = Vector3.ClampMagnitude(mouseDelta, maxDeltaLength);
        
        if (mouseDelta.sqrMagnitude > 0)
        {
            velocity += mouseDelta * power * Time.deltaTime;
        }
        velocity *= drag;

        //Vertical Movement Calc
        if (character.isGrounded)
        {
            gravityScale = 0.1f;
            timeSinceGrounded = 0;
        }
        else
        {
            timeSinceGrounded += Time.deltaTime;
            gravityScale = Mathf.Clamp01(timeSinceGrounded);
        }
        Vector3 finalGravity = gravity * gravityScale;

        //Final Movement
        character.Move(velocity + finalGravity);
    }




}
