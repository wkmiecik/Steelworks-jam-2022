using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;    
    [SerializeField] private LayerMask climbableWall;

    [Header("Climbing Settings")]
    [SerializeField] private float climbSpeed;
    [SerializeField] private float maxClimbTime;

    [Header("Detection")]
    [SerializeField] private float detectionLength;
    [SerializeField] private float sphereCastRadius;
    [SerializeField] private float maxWallLookAngle;

    private float climbTimer;
    private float wallLookAngle;

    private bool isClimbing;
    private bool isWallInFront;

    private RaycastHit frontWallHit;
    private Rigidbody rb;
    private PlayerMovement pm;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        WallCheck();
        StateMachine();
        HandleClimbTimer();
        GroundedCheck();

        if (isClimbing)
        {
            ClimbingMovement();
        }
    }

    private void GroundedCheck()
    {
        if (pm.IsGrounded)
        {
            climbTimer = maxClimbTime;
        }
    }

    private void HandleClimbTimer()
    {
        if(climbTimer > 0)
        {
            climbTimer -= Time.deltaTime;
        }
    }

    private void StateMachine()
    {
        //State 1 Climbing
        if (isWallInFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle)
        {
            if (!isClimbing && climbTimer > 0)
            {
                StartClimbing();
            }
            if (climbTimer < 0)
            {
                StopClimbing();
            }
        }
        else
        {
            if (isClimbing)
            {
                StopClimbing();
            }
        }
    }

    private void WallCheck()
    {
        isWallInFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, climbableWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);        
    }

    private void StartClimbing()
    {
        isClimbing = true;
        pm.SetClimbing(isClimbing);
    }

    private void ClimbingMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);       
    }

    private void StopClimbing()
    {
        isClimbing = false;
        pm.SetClimbing(isClimbing);
    }
}
