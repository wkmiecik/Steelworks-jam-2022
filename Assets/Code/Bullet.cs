using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeAliveAfterHit = 10;
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
        if (rb.velocity.magnitude > 3)
        {
            rb.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
        }

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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Die();
        }

        if (dieAfterHit)
        {
            Destroy(gameObject);
        }

        alreadyHit = true;
    }
}
