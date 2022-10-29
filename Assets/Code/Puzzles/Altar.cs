using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var playerPickUp = other.GetComponent<ItemPickUp>();

        if (playerPickUp != null)
        {
            playerPickUp.closeToAltar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var playerPickUp = other.GetComponent<ItemPickUp>();

        if (playerPickUp != null)
        {
            playerPickUp.closeToAltar = false;
        }
    }
}
