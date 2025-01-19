using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target; // cel przeciwnika (np. gracz)
    public float shootingRange = 15f; // zasięg strzału
    public float fireRate = 1f; // ilość strzałów na sekundę
    public float damage = 10f; // obrażenia zadawane graczowi

    private float nextFireTime = 0f;

    void Update()
    {
        if (target == null) return;

        // celuje w kierunku gracza
        Vector3 directionToTarget = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Gładkie obracanie

        // sprawdza , czy gracz jest w zasięgu
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= shootingRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // ustawia czas do następnego strzału
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        // sprawdza , czy trafiono gracza
        if (Physics.Raycast(transform.position, transform.forward, out hit, shootingRange))
        {
            Debug.Log($"Przeciwnik trafił: {hit.collider.name}");

            // sprawdza , czy trafiony obiekt ma komponent PlayerHealth
            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    // wizualizuje zasięgu strzału w edytorze
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
