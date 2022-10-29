using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]   
    [SerializeField] private Transform cam;
    [SerializeField] private Transform lineOrigin;
    [SerializeField] private LayerMask whatIsGrappleable;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Grappling Setting")]
    [SerializeField] private float maxGrappleDistance;
    [SerializeField] private float grappleDelayTime;
    [SerializeField] private float grapplingCooldown;
    [SerializeField] private float overshootYAxis;
    

    [Header("Input")]
    [SerializeField] private KeyCode grappleKey = KeyCode.Mouse1;

    private float grapplingCooldownTimer;
    private PlayerMovement pm;
    private Vector3 grapplePoint;
    private bool isGrappling;

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

        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }
    }

    private void LateUpdate()
    {
        if (isGrappling)
        {
            lineRenderer.SetPosition(0, lineOrigin.position);
        }
    }

    private void StartGrapple()
    {
        if(grapplingCooldownTimer > 0)
        {
            return;
        }
        isGrappling = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);            
            //pm.SetFreeze(grappling);
        }
        else
        {
            grapplePoint = cam.position + (cam.forward * maxGrappleDistance);
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        //pm.SetFreeze(false);

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if(grapplePointRelativeYPos < 0)
        {
            highestPointOnArc = overshootYAxis;
        }

        pm.JumpToPosition(grapplePoint, highestPointOnArc);
        Invoke(nameof(StopGrapple), 1f);
    }

    private void StopGrapple()
    {
        isGrappling = false;
        //pm.SetFreeze(grappling);
        grapplingCooldownTimer = grapplingCooldown;
        lineRenderer.enabled = false;
    }

}
