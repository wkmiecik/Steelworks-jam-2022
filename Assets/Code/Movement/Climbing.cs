using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Rigidbody rb;
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

    private void WallCheck()
    {
        isWallInFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, climbableWall);
    }
}
