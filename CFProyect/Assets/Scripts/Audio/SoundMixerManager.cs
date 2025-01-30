using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Start()
    {
        LoadVolumeSettings();
    }

    public void SetMusicVolume(float level)
    {
        float volume = Mathf.Log10(level) * 20f;
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat(MusicVolumeKey, level); 
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float level)
    {
        float volume = Mathf.Log10(level) * 20f;
        audioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat(SFXVolumeKey, level); 
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20f);
        }

        if (PlayerPrefs.HasKey(SFXVolumeKey))
        {
            float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20f);
        }
    }
}

