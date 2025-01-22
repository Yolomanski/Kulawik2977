using UnityEngine;

public class EndGameOnTouch : MonoBehaviour
{
    public static bool GameEnd = false;
    public GameObject EndGameOnTouchUi;
    public bool lockCursor = true;
    // Funkcja wywo³ywana, gdy inny obiekt wejdzie w kolizjê z tym obiektem
    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy, czy obiekt, który dotkn¹³ tego obiektu, to gracz
        if (other.CompareTag("Player"))
        {
            // Mo¿esz dodaæ animacjê lub efekty dŸwiêkowe przed zakoñczeniem gry

            // Zakoñczenie gry
            EndGame();
        }
    }

    // Funkcja koñcz¹ca grê
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
