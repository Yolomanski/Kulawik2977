
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 10f; // Ilość obrażeń zadawanych przeciwnikom
    public float range = 100f; // Zasięg strzału
    public GameObject bulletPrefab; // Prefab pocisku
    public Transform firePoint; // Punkt wystrzału pocisku
    public float bulletSpeed = 15f; // Prędkość pocisku
    public Camera playerCamera; // Kamera gracza (do ustalenia kierunku)

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Domyślnie przypisane do lewego przycisku myszy
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Tworzy pocisk w punkcie firePoint
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // Nadaje pociskowi prędkość w kierunku kamery gracza
            if (rb != null)
            {
                Vector3 shootDirection = playerCamera.transform.forward; // Kierunek z kamery
                rb.linearVelocity = shootDirection * bulletSpeed;
            }

            // Niszczy pocisk po określonym czasie, jeśli nie trafi w nic
            Destroy(bullet, 5f);
        }
        else
        {
            Debug.LogWarning("Prefab pocisku lub firePoint nie został przypisany.");
        }
    }
}
