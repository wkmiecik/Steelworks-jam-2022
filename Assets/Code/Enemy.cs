using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
