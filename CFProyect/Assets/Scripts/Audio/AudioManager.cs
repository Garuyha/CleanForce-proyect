using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [Header("----------------- Fuentes de Audio -------------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    
    [Header("----------------- Audio -------------")]
    public AudioClip background;
    public AudioClip bublesLong;
    public AudioClip bublesShort;
    public AudioClip footsChild;
    public AudioClip footsMan;
    public AudioClip win;
    public AudioClip buttom;
    public AudioClip[] counter; 
    public AudioSource adSource;

    private List<AudioSource> allSFXSources = new List<AudioSource>(); // Lista con todos los SFX de la escena

    private void Awake()
    {
        musicSource.Stop();
        musicSource.clip = background;
        StartCoroutine(playAudioSequentially());
        musicSource.Play();

        // Obtener automáticamente todos los AudioSource en la escena (excepto el de la música)
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source != musicSource) // Evitar agregar la música a la lista de SFX
            {
                allSFXSources.Add(source);
            }
        }
    }

    // Reproducir una lista de sonidos secuencialmente
    IEnumerator playAudioSequentially()
    {
        yield return null;
        for (int i = 0; i < counter.Length; i++)
        {
            SFXSource.clip = counter[i];
            SFXSource.Play();
            while (SFXSource.isPlaying)
            {
                yield return null;
            }
        }
    }

    // Método para cambiar el volumen de la música
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // Método para cambiar el volumen de los efectos de sonido (incluyendo los del Player)
    public void SetSFXVolume(float volume)
    {
        foreach (AudioSource source in allSFXSources)
        {
            source.volume = volume;
        }
    }
}

