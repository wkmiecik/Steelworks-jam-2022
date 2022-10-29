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

    [Header("Prediction")]
    public float predictionSphereCastRadius;
    //public Transform predictionPoint;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private float grapplingCooldownTimer;
    private PlayerMovement pm;
    private RaycastHit predictionHit;
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

        CheckForGrapplingPoints();  

        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }

        animator.SetBool("TentacleOut", isGrappling);
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
        if (pm.IsPlayerHoldingItem)
        {
            return;
        }
        isGrappling = true;
        
        if (predictionHit.point != Vector3.zero)
        {
            grapplePoint = predictionHit.point;
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
        Invoke(nameof(StopGrapple), 2f);
    }

    private void StopGrapple()
    {
        isGrappling = false;
        //pm.SetFreeze(grappling);
        grapplingCooldownTimer = grapplingCooldown;
        lineRenderer.enabled = false;
    }
    private void CheckForGrapplingPoints()
    {
        if (isGrappling)
        {
            return;
        }

        RaycastHit raycastHit;
        Physics.Raycast(cam.position, cam.forward, out raycastHit, maxGrappleDistance, whatIsGrappleable);

        RaycastHit sphereCastHit;
        Physics.SphereCast(cam.position, predictionSphereCastRadius, cam.forward, out sphereCastHit, maxGrappleDistance, whatIsGrappleable);

        Vector3 realHitPoint;

        if (raycastHit.point != Vector3.zero)
        {
            realHitPoint = raycastHit.point;
        }
        else if (sphereCastHit.point != Vector3.zero)
        {
            realHitPoint = sphereCastHit.point;
        }
        else
        {
            realHitPoint = Vector3.zero;
        }

        //if (realHitPoint != Vector3.zero)
        //{
        //    predictionPoint.gameObject.SetActive(true);
        //    predictionPoint.transform.position = realHitPoint;

        //}
        //else
        //{
        //    predictionPoint.gameObject.SetActive(false);
        //}

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
    }

}
