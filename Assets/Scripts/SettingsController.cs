using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public AudioSource musicSource;     // Przypisz AudioSource, z kt�rego odtwarzana jest muzyka
    public Toggle musicToggle;          // Toggle do w��czania/wy��czania muzyki
    public Slider volumeSlider;         // Slider do regulacji g�o�no�ci

    void Start()
    {
        // Odczyt stanu z PlayerPrefs
        musicToggle.isOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        // Ustawienie pocz�tkowego stanu Toggle i Slidera na podstawie obecnego stanu AudioSource
        if (musicToggle.isOn)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }

        // Zarejestruj metody dla Toggle i Slidera
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    // Funkcja do w��czania/wy��czania muzyki
    void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }

        // Zapisz stan Toggle do PlayerPrefs
        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
    }

    // Funkcja do zmiany g�o�no�ci
    void ChangeVolume(float volume)
    {
        musicSource.volume = volume;

        // Zapisz warto�� g�o�no�ci do PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
