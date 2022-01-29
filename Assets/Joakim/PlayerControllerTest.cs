using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    private CharacterController character;
    private BodyType bodyType;
    private MeshRenderer meshRenderer;

    private Vector3 velocity;
    [SerializeField] float power = 2;
    [SerializeField] float drag = 0.9f;

    [SerializeField] float maxDeltaLength = 2;

    [SerializeField] Vector3 gravity;

    float timeSinceGrounded = 0;

    float gravityScale = 1f;

    void Start()
    {
        character = GetComponent<CharacterController>();
        bodyType = GetComponent<BodyType>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            ToggleHardness();
        }

        //Horisontal Movement Calc
        velocity += ReadMouseXInput();
        velocity *= drag;

        //Vertical Movement Calc
        SetGravityScale();
        Vector3 finalGravity = gravity * gravityScale;

        //Final Movement
        character.Move(velocity + finalGravity);
    }

    private void ToggleHardness()
    {
        bodyType.hard = !bodyType.hard;
        if (bodyType.hard)
        {
            meshRenderer.material.color = Color.red;
        }
        else
        {
            meshRenderer.material.color = Color.white;
        }
    }

    private void SetGravityScale()
    {
        if (character.isGrounded)
        {
            timeSinceGrounded = 0;
            gravityScale = 0.1f;
        }
        else
        {
            timeSinceGrounded += Time.deltaTime;
            gravityScale = Mathf.Clamp01(timeSinceGrounded);
        }
    }

    private Vector3 ReadMouseXInput()
    {
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), 0, 0);
        mouseDelta = Vector3.ClampMagnitude(mouseDelta, maxDeltaLength);

        if (mouseDelta.sqrMagnitude > 0)
        {
            mouseDelta *= power * Time.deltaTime;
        }
        return mouseDelta;
    }
}
