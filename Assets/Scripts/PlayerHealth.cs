using UnityEngine;
using UnityEngine.SceneManagement; // Do obsługi przechodzenia między scenami

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject deathScreen; // ekran śmierci (UI)
    public float restartDelay = 3f; // czas do restartu gry po śmierci

    private bool isDead = false; // czy gracz już zginął?

    void Start()
    {
        currentHealth = maxHealth;

        // ukrywa ekran śmierci na początku
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return; // gdy gracz już zginął, ignoruj dalsze obrażenia

        currentHealth -= damageAmount;
        Debug.Log($"Gracz otrzymał {damageAmount} obrażeń. Pozostałe HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Gracz zginął!");

        // wyswietla ekran śmierci, przypisz!!!!!!!!!!!!!!!!!!!!!1
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }

        // wylacza ruch gracza
        FirstPersonController FirstPersonController = GetComponent<FirstPersonController>();
        if (FirstPersonController != null)
        {
            FirstPersonController.enabled = false;
        }

        // wywoluje restart gry po określonym czasie
        Invoke("RestartGame", restartDelay);
    }

    void RestartGame()
    {
        // restartuje bieżącą scenę
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

