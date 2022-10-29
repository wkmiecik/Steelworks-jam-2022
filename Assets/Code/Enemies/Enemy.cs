using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] int maxhp = 200;
    int hp;

    void Awake()
    {
        hp = maxhp;
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
}
