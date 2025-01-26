using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Pocisk trafi³ w: {collision.collider.name}");

        EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            Debug.Log($"Zadajê obra¿enia przeciwnikowi: {collision.collider.name}");
            enemyHealth.TakeDamage(damage); // Zadaje obra¿enia
            Destroy(gameObject);           // Usuwa pocisk
        }
        else
        {
            Debug.Log($"Pocisk trafi³ w coœ innego: {collision.collider.name}");
        }
    }
}