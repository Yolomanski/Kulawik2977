
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 10f; // ilość obrażeń zadawanych przeciwnikom
    public float range = 100f; // zasięg promienia
    public Camera playerCamera; // kamera gracza (do obsługi Raycasta)

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // domyślnie przypisane do lewego przycisku myszy
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        // rzucanie promienia od kamery gracza w kierunku środka ekranu
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log($"Trafiono: {hit.collider.name}");

            // sprawdzenie , czy trafiony obiekt ma komponent EnemyHealth
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
