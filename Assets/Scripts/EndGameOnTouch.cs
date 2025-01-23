using UnityEngine;

public class EndGameOnTouch : MonoBehaviour
{
    public static bool GameEnd = false;
    public GameObject EndGameOnTouchUi;
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
        EndGameOnTouchUi.SetActive(true);
        Time.timeScale = 0f;
        GameEnd = true;
        lockCursor = false;
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
