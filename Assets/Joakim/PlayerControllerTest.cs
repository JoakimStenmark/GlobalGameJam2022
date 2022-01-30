using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    private CharacterController character;
    public BodyType bodyType;
    private MeshRenderer meshRenderer;

    private Vector3 velocity;
    public Vector3 Velocity { get => velocity; }
    [Header("Mouse Settings")]
    [SerializeField] float movePower = 2;
    [SerializeField] float maxDeltaLength = 2;
    [SerializeField] Transform arrowIndicator;
    [SerializeField] float mouseDrag = 1;
    [Header("Gravity Settings")]
    [SerializeField] float airDragOut = 0.9f;
    [SerializeField] float airDragShell = 0.0f;
    [SerializeField] float gravityConstant = 10;
    [SerializeField] float maxFallSpeed = 10;

    [SerializeField] Vector3 gravity;
    [SerializeField] float gravityScale = 1f;
    [SerializeField] float timeToMaxGravity = 10f;
    float timeSinceGrounded = 0;

    public Vector3 addVelocity = Vector2.zero;
    public int score;
    public bool allowControls = true;



    public void AddScore(int s)
    {
        score += s;
    }

    void Start()
    {
        character = GetComponent<CharacterController>();
        bodyType = GetComponent<BodyType>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!character.isGrounded)
        {
            if (hit.collider.TryGetComponent(out BreakBrick bb))
            {
                bb.CompairBody(bodyType, hit.point);
            }
            /*else
            if (hit.collider.TryGetComponent(out Fruit mums))
            {
                mums.Eat(this);
            }
            */
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && allowControls)
        {
            ToggleHardness();
        }

        //Drag Air
        if (!bodyType.hard)
            velocity *= 1 - airDragOut * Time.deltaTime;
        else
            velocity *= 1 - airDragShell * Time.deltaTime;
        
        //Horisontal Movement Calc
        if (allowControls)
            velocity.x = AltAltMouseController(velocity.x, bodyType.hard);

        //Vertical Movement Calc
        SetGravityScale();
        Vector3 finalGravity = gravity * gravityScale;



        velocity.y -= gravityConstant * Time.deltaTime;

        velocity.y = Mathf.Clamp(velocity.y, -maxFallSpeed, maxFallSpeed);

        //Inside player area
        float predictedPos = character.transform.position.x + velocity.x * Time.deltaTime;
        if (velocity.x < 0 && predictedPos < ObjectSpawnController.Instance.minBound)
        {
            velocity.x = 0;
        }
        else if (velocity.x > 0 && predictedPos > ObjectSpawnController.Instance.maxBound)
            velocity.x = 0;

        velocity += addVelocity;
        addVelocity = Vector3.zero;

        //Final Movement
        character.Move(velocity * Time.deltaTime);
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
            gravityScale = 0.1f + Mathf.InverseLerp(0, timeToMaxGravity, timeSinceGrounded);
        }
    }

    private float ReadMouseXInput(float velocity)
    {
        float mouseDelta = Input.GetAxis("Mouse X");
        mouseDelta = Mathf.Clamp(mouseDelta, -maxDeltaLength, maxDeltaLength);

        mouseDelta *= movePower * Time.deltaTime;

        return mouseDelta;
    }

    private float AlternetiveMouseController(float veclocityX)
    {
        float midPoint = Screen.width * 0.5f;
        float mouseIn = Input.mousePosition.x;
        mouseIn -= midPoint;

        Vector3 arrowScreenPos = Vector3.one * midPoint;
        arrowScreenPos.y = 10;
        arrowScreenPos.z = 10;
        Vector3 arrowScale = Vector3.one;
        arrowScale.x = -mouseIn * 0.1f;
        arrowIndicator.localScale = arrowScale;
        arrowIndicator.position = Camera.main.ScreenToWorldPoint(arrowScreenPos);

        mouseIn /= midPoint;
        mouseIn = Mathf.Clamp(mouseIn, -maxDeltaLength, maxDeltaLength);
        //Pow
        if (mouseIn < 0)
        {
            mouseIn *= mouseIn;
            mouseIn *= -1;
        }
        else
        {
            mouseIn *= mouseIn * mouseIn;
        }

        //MorePower
        mouseIn *= movePower;
        mouseIn += veclocityX * (1 - mouseDrag * Time.deltaTime);

        Debug.Log(mouseIn);
        return mouseIn;
    }

    float AltAltMouseController(float velocityX, bool hard)
    {
        Vector3 turtleScreenP = Camera.main.WorldToScreenPoint(transform.position);
        float xForce = Input.mousePosition.x - turtleScreenP.x;

        xForce *= movePower * Time.deltaTime;

        if (xForce < 0)
        {
            xForce *= xForce;
            xForce *= -1;
        }
        else
        {
            xForce *= xForce;
        }
        xForce = Mathf.Clamp(xForce, -maxDeltaLength, maxDeltaLength);

        if (hard)
            xForce += velocityX * (1 - mouseDrag * Time.deltaTime *0.25f);
        else
            xForce += velocityX * (1 - mouseDrag * Time.deltaTime);

        return xForce;
    }
}
