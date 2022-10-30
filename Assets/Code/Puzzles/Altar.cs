using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public CapsuleCollider positionCollider;

    public Portal portal;

    private void Start()
    {
        positionCollider = GetComponentInChildren<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerPickUp = other.GetComponentInParent<ItemPickUp>();

        if (playerPickUp != null)
        {
            playerPickUp.closeToAltar = true;
            playerPickUp.closestAltar = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var playerPickUp = other.GetComponentInParent<ItemPickUp>();

        if (playerPickUp != null)
        {
            playerPickUp.closeToAltar = false;
            playerPickUp.closestAltar = this;
        }
    }

    public void TriggerEndPortal()
    {
        portal.Open();
    }
}
