using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Sliders")]
    public Slider sensitivitySlider;
    public Slider volumeSlider;

    [Header("UI Dropdowns")]
    public Dropdown qualityDropdown;
    public Dropdown resolutionDropdown;

    [Header("Audio")]
    public AudioSource audioSource;

    private float defaultSensitivity = 1.0f;
    private float defaultVolume = 1.0f;

    private Resolution[] resolutions;

    void Start()
    {
        // Inicjalizacja suwak�w i ustawienia domy�lne
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", defaultSensitivity);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", defaultVolume);

        // Ustawienia g�o�no�ci
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Ustawienia czu�o�ci myszy
        sensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);

        // Inicjalizacja ustawie� graficznych
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        // Inicjalizacja rozdzielczo�ci
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        foreach (Resolution res in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(res.width + "x" + res.height));
        }
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", resolutions.Length - 1);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // Zastosowanie ustawie�
        ApplySettings();
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void SetMouseSensitivity(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        // Przeka� czu�o�� myszy do kontrolera kamery, je�li jest wymagane
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("QualityLevel", index);
    }

    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);
    }

    public void ApplySettings()
    {
        // Wczytaj zapisane ustawienia i zastosuj je
        SetVolume(PlayerPrefs.GetFloat("Volume", defaultVolume));
        SetMouseSensitivity(PlayerPrefs.GetFloat("MouseSensitivity", defaultSensitivity));
        SetQuality(PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel()));
        SetResolution(PlayerPrefs.GetInt("ResolutionIndex", resolutions.Length - 1));
    }

    public void ResetToDefault()
    {
        sensitivitySlider.value = defaultSensitivity;
        volumeSlider.value = defaultVolume;
        qualityDropdown.value = QualitySettings.names.Length - 1;
        resolutionDropdown.value = resolutions.Length - 1;
        ApplySettings();
    }
}
