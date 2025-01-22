using UnityEngine;

public class EndGameOnTouch : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUi;
    public bool lockCursor = true;
    // Funkcja wywo�ywana, gdy inny obiekt wejdzie w kolizj� z tym obiektem
    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy, czy obiekt, kt�ry dotkn�� tego obiektu, to gracz
        if (other.CompareTag("Player"))
        {
            // Mo�esz doda� animacj� lub efekty d�wi�kowe przed zako�czeniem gry

            // Zako�czenie gry
            EndGame();
        }
    }

    // Funkcja ko�cz�ca gr�
    private void EndGame()
    {
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        lockCursor = true;
        SetCursorState();
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
