using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    BoxCollider damageTrigger;
    PlayerMovement pm;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        damageTrigger = GetComponent<BoxCollider>();
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (agent.isActiveAndEnabled)
            agent.SetDestination(pm.transform.position);
    }

    public void Die()
    {
        StartCoroutine("DyingCoroutine");
    }

    IEnumerator DyingCoroutine()
    {
        // todo: dying animation
        agent.enabled = false;
        damageTrigger.enabled = false;

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var healthSystem = other.GetComponentInParent<HealthSystem>();
            healthSystem.TakeDamege(20);
        }
    }
}
