using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("----------------- Fuente de audio -------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("----------------- Audio ------------")]
    public AudioClip background;
    public AudioClip bublesLong;
    public AudioClip bublesShort;
    public AudioClip footsChild;
    public AudioClip footsMan;
    public AudioClip win;
    public AudioClip buttom;
    public AudioClip[] counter; 
    public AudioSource adSource;

    private void Awake()
    {
        musicSource.Stop();
        musicSource.clip = background;
        StartCoroutine(playAudioSequentially());
        musicSource.Play();
    }


    // every 2 seconds perform the print()
    IEnumerator playAudioSequentially()
    {
        yield return null;

        //1.Loop through each AudioClip
        for (int i = 0; i < counter.Length; i++)
        {
            //2.Assign current AudioClip to audiosource
            SFXSource.clip = counter[i];

            //3.Play Audio
            SFXSource.Play();

            //4.Wait for it to finish playing
            while (SFXSource.isPlaying)
            {
                yield return null;
            }

            //5. Go back to #2 and play the next audio in the adClips array
        }
    }
}
