using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // Obra¿enia zadawane graczowi

    private void OnCollisionEnter(Collision collision)
    {
        // Sprawdza, czy trafiono gracza
        PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage); // Zadaje obra¿enia graczowi
        }

        // Niszczy pocisk po kolizji
        Destroy(gameObject);
        EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage); // Zadaje obra¿enia przeciwnikowi
        }

        // Niszczy pocisk po kolizji
        Destroy(gameObject);
    }
}
