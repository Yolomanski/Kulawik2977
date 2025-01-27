using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksymalne zdrowie gracza
    private int currentHealth;  // Aktualne zdrowie gracza
    public GameObject deathScreen; // Panel UI, który pojawi się po śmierci
    public bool lockCursor = true;

    void Start()
    {
        // Ustawienie początkowego zdrowia
        currentHealth = maxHealth;

        // Ukrycie ekranu śmierci na początku
        deathScreen.SetActive(false);
    }

    void Update()
    {
        // Wykrywanie, czy gracz otrzymuje obrażenia
        if (Input.GetKeyDown(KeyCode.Backspace)) // Testowy input - zmień na własny system obrażeń
        {
            TakeDamage(20);  // Przykładowe obrażenia
        }
    }

    // Funkcja do zadawania obrażeń graczowi
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die(); // Gracz umiera
        }
    }

    // Funkcja wywołana przy śmierci gracza
    private void Die()
    {
        // Wyświetlanie ekranu śmierci
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
        lockCursor = false;
        SetCursorState();
    }

    // Funkcja do leczenia gracza
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    void SetCursorState()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

