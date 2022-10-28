using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]   
    [SerializeField] private Transform cam;
    [SerializeField] private Transform gunTip;
    [SerializeField] private LayerMask whatIsGrappleable;

    [Header("Grappling Setting")]
    [SerializeField] private float maxGrappleDistance;
    [SerializeField] private float grappleDelayTime;
    [SerializeField] private float grapplingCooldown;
    [SerializeField] private float grapplingCooldownTimer;

    [Header("Input")]
    [SerializeField] private KeyCode grappleKey = KeyCode.Mouse1;

    private PlayerMovement pm;
    private Vector3 grapplePoint;
    private bool grappling;

    private void Awake()
    {
        pm = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if(grapplingCooldownTimer > 0)
        {
            grapplingCooldownTimer -= Time.deltaTime;   
        }
    }

    private void StartGrapple()
    {
        if(grapplingCooldown > 0)
        {
            return;
        }

        grappling = true;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + (cam.forward * maxGrappleDistance);
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }
    }

    private void ExecuteGrapple()
    {

    }

    private void StopGrapple()
    {
        grappling = false;
        grapplingCooldownTimer = grapplingCooldown;
    }

}
