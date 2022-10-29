using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;

    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
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
}
