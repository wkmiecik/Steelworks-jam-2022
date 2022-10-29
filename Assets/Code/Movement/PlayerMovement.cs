using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{
    public bool IsGrounded => isGrounded;
    [field: SerializeField]public MovementState State { get; set; }
   

    [Header("Movement")]
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float horizontalSpeedDuringClimb;
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
    private Vector3 moveDirection;
    private float moveSpeed;

    //SF FOR TESTING
    private bool isGrounded;
    private bool isReadyToJump = true;
    private bool isClimbing;    

    private Rigidbody rb;

    private RaycastHit slopeHit;

    public enum MovementState
    {
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

    public void SetClimbing(bool value)
    {
        isClimbing = value;      
    }

    private void StateHandler()
    {
        if (isClimbing)
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
        if (isGrounded)
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
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed / 10f)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed / 10f;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        
    }

    private void UpdateSpeedText()
    {
        speedText.SetText($"Speed: {rb.velocity.magnitude}");
    }    
}
