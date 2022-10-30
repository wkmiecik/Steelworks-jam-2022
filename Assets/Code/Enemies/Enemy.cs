using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(GameManager.instance.PlayerCameraPos);
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
