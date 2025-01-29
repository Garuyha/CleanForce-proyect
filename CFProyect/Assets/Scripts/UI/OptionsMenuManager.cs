using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel; // Panel del menú de opciones
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private AudioSource musicSource;

    private void Start()
    {
        // Obtener la fuente de audio de la música (asume que hay un AudioSource en el GameObject)
        musicSource = FindObjectOfType<AudioSource>();

        // Cargar valores guardados
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Aplicar volumen inicial
        UpdateMusicVolume();
        UpdateSFXVolume();

        // Agregar listeners a los sliders
        musicSlider.onValueChanged.AddListener(delegate { UpdateMusicVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate { UpdateSFXVolume(); });

        // Asegurar que el menú inicie oculto
        optionsPanel.SetActive(false);
    }

    private void UpdateMusicVolume()
    {
        float volume = musicSlider.value;
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    private void UpdateSFXVolume()
    {
        float volume = sfxSlider.value;
        // Aquí puedes configurar el volumen de efectos de sonido si tienes un AudioManager centralizado
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void ToggleOptionsMenu()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }
}
