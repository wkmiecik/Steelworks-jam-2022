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

    private Vector3 swingPoint;
    private Vector3 currentGrapplePosition;
    private RaycastHit hit;
    private SpringJoint joint;
    private PlayerMovement pm;
    private bool isSwinging;

    [Header("Input")]
    [SerializeField] private KeyCode swingKey = KeyCode.Mouse1;

    private void Awake()
    {
        pm = GetComponent<PlayerMovement>();    
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
    }

    private void LateUpdate()
    {
         DrawRope();        
    }

    private void StartSwing()
    {
        isSwinging = true;
        pm.SetSwing(isSwinging);

        if(Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, whatIsSwingable))
        {
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            //lineRenderer.positionCount = 2;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(1, swingPoint);

            currentGrapplePosition = lineOrigin.position;        
        }   
    }
    private void StopSwing()
    {
        isSwinging = false;
        pm.SetSwing(isSwinging);

        //lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;

        Destroy(joint);
    }

    private void DrawRope()
    {
        if (isSwinging)
        {
            lineRenderer.SetPosition(0, lineOrigin.position);
        }
    }
}
