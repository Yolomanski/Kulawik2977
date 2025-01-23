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
        // Automatically find the player if target is not assigned
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogError("No object with 'Player' tag found. Assign a target manually.");
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
        if (bulletPrefab == null || shootingPoint == null || target == null)
        {
            Debug.LogWarning("Missing bulletPrefab, shootingPoint, or target. Cannot shoot.");
            return;
        }

        // Calculate the direction to the target
        Vector3 direction = (target.position - shootingPoint.position).normalized;

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Debug.Log("Bullet instantiated at: " + shootingPoint.position);

        // Add force to the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
            Debug.Log("Bullet fired toward target with velocity: " + rb.linearVelocity);
        }
        else
        {
            Debug.LogError("Bullet prefab is missing a Rigidbody component.");
        }
    }
}
