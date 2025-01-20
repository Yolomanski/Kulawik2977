using UnityEngine;

public class DeathScreen : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUi;
    public bool lockCursor = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    void Resume()
    {
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        lockCursor = true;
    }
    void Pause()
    {
        PauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        lockCursor = false;
    }
}
