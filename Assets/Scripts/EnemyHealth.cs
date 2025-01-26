using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"{gameObject.name} otrzymał {damage} obrażeń.");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} zginął. Wywołuję Die().");
            Die();
        }
        else
        {
            Debug.Log($"Pozostałe zdrowie przeciwnika: {currentHealth}");
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} został zniszczony!");
        Destroy(gameObject);
    }
}