using UnityEngine;
using UnityEngine.UI;

public class ObjectTouchManager : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject[] screens; // Tablica ekranów (panele UI)
    [SerializeField] private Camera mainCamera;    // G³ówna kamera (do raycasta)

    [Header("End Game Settings")]
    public static bool GameEnd = false;
    public GameObject EndGameOnTouchUi; // UI koñca gry
    public bool lockCursor = true;

    private void Update()
    {
        // SprawdŸ, czy dotkniêto ekranu na urz¹dzeniu dotykowym
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // Dotkniêcie rozpoczête
            {
                Ray ray = mainCamera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    HandleObjectTouch(hit.collider.gameObject);
                }
            }
        }

        // Alternatywa dla testowania na komputerze (klik myszk¹)
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                HandleObjectTouch(hit.collider.gameObject);
            }
        }
    }

    private void HandleObjectTouch(GameObject touchedObject)
    {
        // Obs³uga dotkniêcia obiektów
        if (touchedObject.CompareTag("Player"))
        {
            EndGame(); // Zakoñcz grê, jeœli obiekt dotkn¹³ gracza
        }
        else
        {
            // SprawdŸ nazwê obiektu i aktywuj odpowiedni ekran
            switch (touchedObject.name)
            {
                case "Object1":
                    ActivateScreen(0); // Aktywuj ekran 0
                    break;
                case "Object2":
                    ActivateScreen(1); // Aktywuj ekran 1
                    break;
                default:
                    Debug.Log("Nieznany obiekt dotkniêty: " + touchedObject.name);
                    break;
            }
        }
    }

    private void ActivateScreen(int screenIndex)
    {
        // Wy³¹cz wszystkie ekrany
        foreach (var screen in screens)
        {
            screen.SetActive(false);
        }

        // Aktywuj wybrany ekran
        if (screenIndex >= 0 && screenIndex < screens.Length)
        {
            screens[screenIndex].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // SprawdŸ, czy obiekt, który dotkn¹³ tego obiektu, to gracz
        if (other.CompareTag("Player"))
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        EndGameOnTouchUi.SetActive(true);
        Time.timeScale = 0f;
        GameEnd = true;
        lockCursor = false;
        SetCursorState();
    }

    private void SetCursorState()
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
