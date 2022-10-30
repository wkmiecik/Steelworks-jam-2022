using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetTrigger("EnemyTrigger1");
            animator.SetTrigger("EnemyTrigger2");
        }
    }
}
