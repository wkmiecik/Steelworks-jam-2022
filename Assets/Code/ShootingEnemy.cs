using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private float detectionRadious = 50f;
    [SerializeField] private float timeToShoot = 8f;
    [SerializeField] private float timeToSearch = 1f;
    [SerializeField] private float waitingForAnim = 1f;
    [SerializeField] private float bulletForce;

    [SerializeField] private Transform spawn;
    [SerializeField] private GameObject ball;


    private float timerToShoot;
    private Animator animator;
    [SerializeField] private float timerSearch;
    private float animTimer;
    private bool isAbleToShoot = true;
    private bool canSpawn = false;
    private Vector3 targetPos;

    private void Awake()
    {        
        timerSearch = timeToSearch;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(timerToShoot > 0)
        {
            timerToShoot -= Time.deltaTime;
        }
        else
        {
            isAbleToShoot = true;
        }

        if(animTimer > 0)
        {
            animTimer -= Time.deltaTime;
        }
        else
        {            
            SpawnBall();            
        }
      

        if(timerSearch > 0)
        {
            timerSearch -= Time.deltaTime;
        }
        else
        {
            CheckForPlayer();
            timerSearch += timeToSearch;
        }        
    }

    private void CheckForPlayer()
    {
        if (!isAbleToShoot)
        {
            return;
        }
        Vector3 playerPos = GameManager.instance.player.transform.position;
        if (Vector3.Distance(transform.position, playerPos) > detectionRadious)
        {
            return;
        }
        else
        {
            transform.LookAt(playerPos);
            Vector3 dir = (playerPos - transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, detectionRadious))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isAbleToShoot = false;
                    targetPos = dir - Vector3.up;
                    Shoot();
                }
            }
            return;
        }
    }

    private void Shoot()
    {        
        timerToShoot = timeToShoot;
        animator.SetTrigger("Shoot");
        animTimer = waitingForAnim;
        canSpawn = true;
    }

    private void SpawnBall()
    {
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dir = (playerPos - transform.position);
        targetPos = dir - Vector3.up;
        if (!canSpawn)
        {
            return;
        }

        Rigidbody rb = Instantiate(ball, spawn.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(targetPos * bulletForce);
        canSpawn = false;
    }
}
