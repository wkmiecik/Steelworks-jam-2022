using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletForce;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Rigidbody rb = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation).GetComponent<Rigidbody>();
            rb.AddRelativeForce(Vector3.forward * bulletForce);
        }
    }
}
