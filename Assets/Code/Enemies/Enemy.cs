using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    PlayerMovement pm;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        agent.SetDestination(pm.transform.position);
    }

    public void Die()
    {
        StartCoroutine("DyingCoroutine");
    }

    IEnumerator DyingCoroutine()
    {
        // todo: dying animation

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var healthSystem = other.GetComponentInParent<healthSystem>();
            healthSystem.TakeDamege(20);
        }
    }
}
