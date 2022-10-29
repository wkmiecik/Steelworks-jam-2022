using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOnTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Moveable")
        {
            animator.SetTrigger("GateTrigger");
        }
    }
}
