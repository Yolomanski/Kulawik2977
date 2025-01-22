using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab; // The bullet prefab to instantiate
    public Transform shootingPoint; // The point where bullets are spawned
    public float shootingInterval = 2f; // Time between shots
    public float bulletSpeed = 10f; // Speed of the bullet

    [Header("Target Settings")]
    public Transform target; // The target to shoot at

    private float shootingTimer;

    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
        shootingTimer = shootingInterval;
    }

    void Update()
    {
        shootingTimer -= Time.deltaTime;

        if (shootingTimer <= 0f)
        {
            Shoot();
            shootingTimer = shootingInterval;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && shootingPoint != null && target != null)
        {
            // Calculate direction to the target
            Vector3 direction = (target.position - shootingPoint.position).normalized;

            // Instantiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

            // Add force to the bullet
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
            }
        }
        else
        {
            Debug.LogWarning("Missing components or target for shooting.");
        }
    }
}
