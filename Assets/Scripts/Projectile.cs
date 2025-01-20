using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f; // Prędkość pocisku
    public float lifetime = 5f; // Czas życia pocisku
    public GameObject hitEffect; // Efekt po trafieniu (opcjonalny)

    private void Start()
    {
        // Automatyczne zniszczenie pocisku po określonym czasie
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Przemieszczanie pocisku do przodu
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzenie, czy pocisk trafił przeciwnika
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Trafiono przeciwnika!");

            // Możesz dodać tutaj obrażenia dla przeciwnika
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10); // Przykład zadania obrażeń
            }
        }

        // Dodanie efektu po trafieniu (opcjonalne)
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        // Zniszczenie pocisku
        Destroy(gameObject);
    }
}
