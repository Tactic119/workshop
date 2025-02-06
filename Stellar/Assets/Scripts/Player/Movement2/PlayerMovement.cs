using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;
    public float wallrunspeed;

    public float desiredMoveSpeed;
    public float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;
    public float soarIncreaseMultiuplier;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;


   [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGrounded;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;
    public Transform orientationSoaring;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;





    public AudioClip ac;
    


    public enum MovementState
    {
        walking,
        sprinting,
        wallrunning,
        crouching,
        sliding,
        air,
        hovering,
        flying
    }

    public bool sliding;
    public bool wallrunning;

    [Header("Flying")]
    public MovementState state;
    public Flying flying;
    public float hoveringDrag;
    public float startHoverCooldown;
    public float counter;

    private GameObject wingsuit;
    public Anim anim;

    public GameObject playerModel;
    public GameObject camHolder;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;

        flying = GetComponent<Flying>();

        wingsuit = GameObject.FindGameObjectWithTag("Wingsuit");
        anim = wingsuit.GetComponent<Anim>();
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        camHolder = GameObject.FindGameObjectWithTag("CameraHolder");



        
    }

    private void Update()
    {
        // ground check
        if (flying.airBorne == false) grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGrounded);
        else grounded = false;

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else if(flying.hovering == true)
            rb.drag = hoveringDrag;
        else 
            rb.drag = 0;
        
        if(startHoverCooldown > 0)
        {
            startHoverCooldown -= Time.deltaTime;
        }

        if (flying.soaring)
        {
            if(counter <= 0)
            {
                rb.velocity = new Vector3(0, 0, 0);
                counter = 0.2f;
            }
            else counter -= Time.deltaTime;


        }
    }

    private void FixedUpdate()
    {
        MovePLayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // take off
        if (Input.GetKey(KeyCode.F) && !flying.airBorne && wallrunning == false && startHoverCooldown <= 0)
        {
            state = MovementState.hovering;
            flying.hovering = true;
            flying.airBorne = true;
            desiredMoveSpeed = flying.hoverSpeed;
            grounded = false;
            startHoverCooldown = 0.2f;
            flying.TakeOff();
            anim.TakeOffAnim();

        }

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded && !flying.airBorne)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

        }

        // start crouching
        if(Input.GetKeyDown(crouchKey) && !flying.airBorne)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

        
    }

    private void StateHandler()
    {

        // Mode - Wallrunning
        if (wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallrunspeed;
        }

        // Mode - Sliding
        if(sliding)
        {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideSpeed;

            else
                desiredMoveSpeed = sprintSpeed;
        }

        // Mode - Crouching
        else if(Input.GetKey(crouchKey) && !flying.airBorne)
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }

        // Mode - Sprinting
        else if(grounded && Input.GetKey(sprintKey) && !flying.airBorne)
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded && !flying.airBorne)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }

        // Mode - Air
        else if(!flying.airBorne) 
        {
            state = MovementState.air;
        }

        // check is desiredMoveSpeed has chnaged drasitcally
        if(Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            
            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else if (flying.soaring)
            {
                time += Time.deltaTime * soarIncreaseMultiuplier;
            }
                else time += Time.deltaTime * speedIncreaseMultiplier;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePLayer()
    {
        // calculate movement direction
        if(flying.soaring)
        {
            moveDirection = orientationSoaring.forward * verticalInput + orientation.right * horizontalInput;
        }
        else moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if( OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded && flying.airBorne == false)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        else if(flying.airBorne == true && flying.hovering == true)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(flying.airBorne && flying.soaring)
        {
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        }

        // trun gravity off while on slope
        if(!wallrunning && flying.airBorne == false) rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting on slope
        if(OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); //ForceMode.Impule applies foce only once
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}
