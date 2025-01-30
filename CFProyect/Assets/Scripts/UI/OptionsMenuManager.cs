using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel; // Panel del menú de opciones
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    

    private void Start()
    {
        
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 1f); // Valor por defecto: 1
        sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        // Asegurar que el menú inicie oculto
        optionsPanel.SetActive(false);
    }

  

    public void ToggleOptionsMenu()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }
}
