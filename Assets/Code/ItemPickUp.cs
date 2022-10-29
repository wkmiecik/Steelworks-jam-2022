using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [Header("PickUp Settings")]
    [SerializeField] private float pickUpRange;
    [SerializeField] private float itemMoveSpeed;
    [SerializeField] private KeyCode pickUpKey = KeyCode.E;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform holdPlace;
    [SerializeField] private LayerMask pickupable;

    [SerializeField] private GameObject heldItem;
    private RaycastHit physicHit;

    private Rigidbody heldItemRigidbody;
    private Collider itemCollider;
    private PlayerMovement pm;
    private bool isHoldingItem;

    private void Awake()
    {
        pm = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        HandleInput();        
        if (heldItem != null)
        {
            MoveObject();
        }
    }

    private void HandleInput()
    {
        if (!Input.GetKeyDown(pickUpKey))
        {
            return;
        }
        if (pm.IsRightArmBusy)
        {
            return;
        }
        if(heldItem != null)
        {
            DropObject();
        }
        else
        {
            CheckRayCast();
        }
    }

    private void CheckRayCast()
    {
        if (Physics.Raycast(cam.position, cam.forward, out physicHit, pickUpRange, pickupable))
        {
            PickupObject(physicHit.transform.gameObject);
        }
    }

    private void PickupObject(GameObject pickedObject)
    {
        isHoldingItem = true;
        pm.SetItemHolding(isHoldingItem);
        heldItemRigidbody = pickedObject.GetComponent<Rigidbody>();
        itemCollider = heldItemRigidbody.GetComponent<Collider>();

        heldItemRigidbody.useGravity = false;
        heldItemRigidbody.drag = 10;
        itemCollider.isTrigger = true;
       
        heldItem = pickedObject;
        heldItem.transform.parent = holdPlace;
    }

    private void DropObject()
    {
        isHoldingItem = false;
        pm.SetItemHolding(isHoldingItem);
        heldItemRigidbody.useGravity = true;
        heldItemRigidbody.drag = 0;
        itemCollider.isTrigger = false;
        heldItem.transform.parent = null;

        heldItem = null;
    }

    private void MoveObject()
    {
        if (Vector3.Distance(heldItem.transform.position, holdPlace.transform.position) > 0.01f)
        {
            Vector3 moveDircetion = (holdPlace.position - heldItem.transform.position);
            heldItemRigidbody.AddForce(moveDircetion * itemMoveSpeed);
        }
    }
}
