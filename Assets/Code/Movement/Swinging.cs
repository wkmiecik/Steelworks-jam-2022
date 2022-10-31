using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform lineOrigin;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;

    [Header("Swinging Settings")]
    [SerializeField] private LayerMask whatIsSwingable;
    [SerializeField] private float maxSwingDistance = 25f;
    [SerializeField] private float swingDelayTime = 0.15f;

    [Header("SwingControl")]
    [SerializeField] private Transform orientation;
    [SerializeField] private float horizontalThrustForce;
    [SerializeField] private float forwardThrustForce;
    [SerializeField] private float extendCable;

    [Header("Swing Input")]
    [SerializeField] private KeyCode swingLeft = KeyCode.A;
    [SerializeField] private KeyCode swingRight = KeyCode.D;
    [SerializeField] private KeyCode swingForward = KeyCode.W;
    [SerializeField] private KeyCode longerRope = KeyCode.LeftShift;
    [SerializeField] private KeyCode shorternRope = KeyCode.LeftControl;

    [Header("Prediction")]   
    public float predictionSphereCastRadius;
    //public Transform predictionPoint;

    private Vector3 swingPoint;
    private Vector3 hookPosition;
    private RaycastHit predictionHit;
    [SerializeField] private SpringJoint joint;
    private PlayerMovement pm;
    private Rigidbody rb;
    private bool isSwinging;
    private bool isJoint;

    [Header("Input")]
    [SerializeField] private KeyCode swingKey = KeyCode.Mouse1;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private void Awake()
    {
        pm = GetComponent<PlayerMovement>();   
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(swingKey))
        {
            StartSwing();           
        }
        if (Input.GetKeyUp(swingKey))
        {
            StopSwing();
        }

        CheckForSwingPoints();

        if (isSwinging && isJoint)
        {
            SwingMovement();
        }

        animator.SetBool("TentacleOut", isSwinging);
    }

    private void LateUpdate()
    {
         DrawRope();        
    }

    private void InitSwing()
    {
        if (!isSwinging)
        {
            return;
        }
        pm.SetSwing(isSwinging);
        

        joint = player.gameObject.AddComponent<SpringJoint>();
        isJoint = true;
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;
       
    }

    private void StartSwing()
    {
        if(joint != null)
        {
            isJoint = false;
            Destroy(joint);
        }
        else
        {
            joint = GetComponent<SpringJoint>();
            if(joint != null) 
                Destroy(joint);
        }  
        
        if (predictionHit.point == Vector3.zero)
        {
            return;
        }
        if (pm.IsPlayerHoldingItem)
        {
            return;
        }   

        swingPoint = predictionHit.point;
        lineRenderer.positionCount = 2;
        hookPosition = lineOrigin.transform.position;
        lineRenderer.enabled = true;
        isSwinging = true;

        Invoke(nameof(InitSwing), swingDelayTime);

    }
    private void StopSwing()
    {
        isSwinging = false;        
        pm.SetSwing(isSwinging);

        lineRenderer.positionCount = 0;        
        lineRenderer.enabled = false;

        isJoint = false;
        Destroy(joint);        
    }

    private void DrawRope()
    {
        if (isSwinging)
        {
            lineRenderer.SetPosition(0, lineOrigin.position);
            lineRenderer.SetPosition(1, hookPosition);
            float lineSpeedMultiplayer = Mathf.Clamp((Vector3.Distance(lineOrigin.transform.position, swingPoint) / 6f), 2.5f, maxSwingDistance / 6f);
            hookPosition = Vector3.Lerp(hookPosition, swingPoint, Time.deltaTime * lineSpeedMultiplayer);
        }
    }

    private void SwingMovement()
    {
        if (Input.GetKey(swingRight))
        {
            rb.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime);
        }
        if (Input.GetKey(swingLeft))
        {
            rb.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime);
        }
        if (Input.GetKey(swingForward))
        {
            rb.AddForce(orientation.forward * forwardThrustForce * Time.deltaTime);
        }

        if (Input.GetKey(longerRope))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            rb.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }
        if (Input.GetKey(shorternRope))
        {
            float distanceFromPoint = Vector3.Distance(player.position, swingPoint) + extendCable;

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }
    }

    private void CheckForSwingPoints()
    {
        if (isSwinging)
        {
            return;
        }

        RaycastHit raycastHit;
        Physics.Raycast(cam.position, cam.forward, out raycastHit, maxSwingDistance, whatIsSwingable);

        RaycastHit sphereCastHit;
        Physics.SphereCast(cam.position, predictionSphereCastRadius, cam.forward, out sphereCastHit, maxSwingDistance, whatIsSwingable);

        Vector3 realHitPoint;

        if(raycastHit.point != Vector3.zero)
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

        //if(realHitPoint != Vector3.zero)
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
