using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Shooting : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] VisualEffect vfx;
    [SerializeField] float bulletForce;
    [SerializeField] float shootingDelay = 0.6f;

    AudioSource audioSource;
    float cooldown;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cooldown = shootingDelay;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0)  && cooldown <= 0 && !GameManager.instance.isInputLocked)
        {
            cooldown = shootingDelay;

            vfx.Play();

            audioSource.Play();

            animator.SetTrigger("Shoot");

            Rigidbody rb = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation).GetComponent<Rigidbody>();
            rb.AddRelativeForce(Vector3.forward * bulletForce);
        }
    }
}
