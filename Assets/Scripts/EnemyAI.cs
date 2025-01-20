using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target; // Cel przeciwnika (np. gracz)
    public float shootingRange = 15f; // Zasięg strzału
    public float fireRate = 1f; // Ilość strzałów na sekundę
    public GameObject bulletPrefab; // Prefab pocisku
    public Transform firePoint; // Punkt wystrzału pocisku
    public float bulletSpeed = 20f; // Prędkość pocisku

    private float nextFireTime = 0f;

    void Update()
    {
        if (target == null) return;

        // Celuje w kierunku gracza
        Vector3 directionToTarget = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Gładkie obracanie

        // Sprawdza, czy gracz jest w zasięgu
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= shootingRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Ustawia czas do następnego strzału
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Tworzy pocisk w punkcie firePoint
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // Nadaje pociskowi prędkość w kierunku celu
            if (rb != null)
            {
                Vector3 direction = (target.position - firePoint.position).normalized;
                rb.linearVelocity = direction * bulletSpeed;
            }

            // Niszczy pocisk po 5 sekundach, jeśli nie trafił w nic
            Destroy(bullet, 5f);
        }
        else
        {
            Debug.LogWarning("Prefab pocisku lub firePoint nie został przypisany.");
        }
    }
}
