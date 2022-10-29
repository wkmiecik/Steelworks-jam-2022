using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletForce;
    [SerializeField] float shootingDelay = 0.6f;

    float cooldown;

    void Start()
    {
        cooldown = shootingDelay;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0)  && cooldown <= 0)
        {
            cooldown = shootingDelay;

            //animator.Play("Shooting");
            animator.SetTrigger("Shoot");
            //animator.ResetTrigger("Shoot");

            Rigidbody rb = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation).GetComponent<Rigidbody>();
            rb.AddRelativeForce(Vector3.forward * bulletForce);
        }
    }
}
