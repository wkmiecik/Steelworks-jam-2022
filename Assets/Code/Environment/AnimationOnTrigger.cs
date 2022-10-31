using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOnTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private string hantleName;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.tag == "Moveable" && other.gameObject.name.Equals(hantleName))
        {
            animator.SetTrigger("OpenGate");
        }
    }
}
