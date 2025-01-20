using UnityEngine;

public class EndGameOnTouch : MonoBehaviour
{
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
        // Mo¿esz dodaæ efekt przejœcia, np. animacjê, efekt dŸwiêkowy, itp.
        // Na przyk³ad wyœwietlenie ekranu przejœcia, a potem zakoñczenie gry

        // Dodanie prostego komunikatu przed zamkniêciem gry (dla testów)
        Debug.Log("Gra zakoñczona! Dziêkujemy za grê!");

        // Zamkniêcie gry (dzia³a tylko w wersji zbudowanej, nie w edytorze)
        Application.Quit();

        // Jeœli chcesz wyjœæ z edytora Unity (dzia³a tylko w edytorze)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
