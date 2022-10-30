using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var healthSystem = other.GetComponentInParent<healthSystem>();
            healthSystem.TakeDamege(100);
        }
    }
}
