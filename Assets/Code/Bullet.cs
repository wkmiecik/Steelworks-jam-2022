using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeAliveAfterHit = 10;

    float timer;
    bool alreadyHit = false;

    void Start()
    {
        timer = timeAliveAfterHit;
    }

    void Update()
    {
        if (alreadyHit)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0 || transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        alreadyHit = true;
    }
}
