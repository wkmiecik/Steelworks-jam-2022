using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Player"))
        {
            var healthSystem = other.GetComponentInParent<HealthSystem>();
            healthSystem.TakeDamege();
        }
    }
}
