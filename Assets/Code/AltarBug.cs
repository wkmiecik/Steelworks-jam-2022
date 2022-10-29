using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarBug : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PickedUp()
    {
        animator.SetTrigger("Pickup");
    }
}
