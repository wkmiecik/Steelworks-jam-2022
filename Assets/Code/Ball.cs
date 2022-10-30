using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float timeAliveAfterHit = 3;
    [SerializeField] bool dieAfterHit = true;

    Rigidbody rb;
    float timer;
    bool alreadyHit = false;

    void Start()
    {
        timer = timeAliveAfterHit;
        rb = GetComponent<Rigidbody>();
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
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if(healthSystem != null)
            {
                healthSystem.TakeDamege(40);
            }
        }

        if (dieAfterHit)
        {
            Destroy(gameObject);
        }

        alreadyHit = true;
    }
}
