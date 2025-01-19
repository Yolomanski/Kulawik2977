using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab pocisku
    public Transform firePoint; // Punkt wystrzału

    public void Update()
    {
        // Wystrzał pocisku po naciśnięciu lewego przycisku myszy
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Tworzenie pocisku w punkcie wystrzału
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
