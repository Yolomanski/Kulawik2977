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
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("BulletPrefab lub FirePoint nie jest przypisany!");
            return;
        }

        // Tworzy pocisk
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Ignoruj kolizje między pociskiem a przeciwnikiem
        Collider enemyCollider = GetComponent<Collider>();
        Collider bulletCollider = bullet.GetComponent<Collider>();

        if (enemyCollider != null && bulletCollider != null)
        {
            Physics.IgnoreCollision(bulletCollider, enemyCollider);
        }

        // Nadaje pociskowi prędkość w kierunku ognia
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 shootingDirection = (target.position - firePoint.position).normalized;
            rb.linearVelocity = shootingDirection * bulletSpeed;

            Debug.Log($"Pocisk wystrzelony w kierunku: {shootingDirection}");
        }
        else
        {
            Debug.LogWarning("Prefab pocisku nie posiada Rigidbody!");
        }
    }
}
