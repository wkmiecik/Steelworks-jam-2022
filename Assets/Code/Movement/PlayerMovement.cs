using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{
    public bool IsGrounded => isGrounded;
    public bool IsPlayerHoldingItem => isPlayerHoldingItem;
    public bool IsRightArmBusy => isRightArmBusy;

    [field: SerializeField]public MovementState State { get; set; }

    public bool IsPlayerMoving
    {
        get => isGrounded && rb.velocity.magnitude > 0.1f;
    }
   

    [Header("Movement")]
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float horizontalSpeedDuringClimb;
    [SerializeField] private float swingMoveSpeed;
    [SerializeField] private float groundDrag;
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplayer;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Slope Handling")]
    [SerializeField] private float maxSlopeAngle;

    [SerializeField] private Transform orientation;
    [SerializeField] private TextMeshProUGUI speedText;


    private float horizontalInput;
    private float verticalInput;
    private float moveSpeed;
    private Vector3 moveDirection;
    private Vector3 velocityToSet;
   

    //SF FOR TESTING
    private bool isGrounded;
    private bool isReadyToJump = true;
    private bool isClimbing;
    private bool isFrozen;
    private bool isGrappleActive;
    private bool isSwinging;
    private bool isPlayerHoldingItem;
    private bool isRightArmBusy;

    private Rigidbody rb;

    private RaycastHit slopeHit;

    public enum MovementState
    {
        freeze,
        grappling,
        swinging,
        walking,
        climbing,
        air
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        GroundCheck();

        if (!GameManager.instance.isInputLocked) 
            GetKeyboardInput();

        SpeedControl();
        StateHandler();
        ApplyDrag();
        UpdateSpeedText();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        EndGrapplingMoveFreeze();
    }
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        isGrappleActive = true;
        isRightArmBusy = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetGrappleVelocity), 0.05f);
        Invoke(nameof(EndGrapplingMoveFreeze), 1f);
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));
        
       
        return velocityXZ + velocityY;        
    }

    public void SetClimbing(bool value)
    {
        isClimbing = value;      
    }

    public void SetFreeze(bool value)
    {
        isFrozen = value;
    }

    public void SetSwing(bool value)
    {
        isSwinging = value;
        isRightArmBusy = value;
    }

    public void SetItemHolding(bool value)
    {
        isPlayerHoldingItem = value;
    }

    private void StateHandler()
    {
        if (isFrozen)
        {
            State = MovementState.freeze;
            moveSpeed = 0f;
            rb.velocity = Vector3.zero;
        }
        else if (isGrappleActive)
        {
            State = MovementState.grappling;
            moveSpeed = walkingSpeed;
        }
        else if (isSwinging)
        {
            State = MovementState.swinging;
            moveSpeed = swingMoveSpeed;
        }
        else if (isClimbing)
        {            
            State = MovementState.climbing;
            moveSpeed = horizontalSpeedDuringClimb;            
        }
        else if (isGrounded)
        {
            State = MovementState.walking;
            moveSpeed = walkingSpeed;
        }
        else
        {
            State = MovementState.air;
            moveSpeed = walkingSpeed * airMultiplayer;
        }
    }

    private void GetKeyboardInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && isReadyToJump && isGrounded)
        {
            isReadyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        if (isGrappleActive)
        {
            return;
        }
        if (isSwinging)
        {
            return;
        }

        //get walk direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
       
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplayer, ForceMode.Force);
        }        
    }

    private void Jump()
    {       
        rb.velocity = new Vector3(rb.velocity.x * airMultiplayer, 0f, rb.velocity.z * airMultiplayer);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        isReadyToJump = true;        
    }

    private void ApplyDrag()
    {
        if (isGrounded && !isGrappleActive)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void GroundCheck()
    {
        if(State == MovementState.climbing)
        {
            return;
        }
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void SpeedControl()
    {
        if (isGrappleActive)
        {
            return;
        }

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed / 10f)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed / 10f;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        
    }

    private void SetGrappleVelocity()
    {        
        rb.velocity = velocityToSet;
    }
    private void EndGrapplingMoveFreeze()
    {
        isGrappleActive = false;
        isRightArmBusy = false;
    }

    private void UpdateSpeedText()
    {
        speedText.SetText($"Speed: {rb.velocity.magnitude}");
    }    
}
