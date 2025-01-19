using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f; // maksymalne HP przeciwnika
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // ustawia początkowe zdrowie na maksymalne
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"{gameObject.name} otrzymał {damageAmount} obrażeń. Pozostałe HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} został zniszczony.");
        Destroy(gameObject); // usuwa przeciwnika ze sceny
    }
}
