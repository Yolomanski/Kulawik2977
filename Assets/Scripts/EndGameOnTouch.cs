using UnityEngine;

public class EndGameOnTouch : MonoBehaviour
{
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
        // Mo�esz doda� efekt przej�cia, np. animacj�, efekt d�wi�kowy, itp.
        // Na przyk�ad wy�wietlenie ekranu przej�cia, a potem zako�czenie gry

        // Dodanie prostego komunikatu przed zamkni�ciem gry (dla test�w)
        Debug.Log("Gra zako�czona! Dzi�kujemy za gr�!");

        // Zamkni�cie gry (dzia�a tylko w wersji zbudowanej, nie w edytorze)
        Application.Quit();

        // Je�li chcesz wyj�� z edytora Unity (dzia�a tylko w edytorze)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
