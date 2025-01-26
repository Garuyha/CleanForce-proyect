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

    private void Awake()
    {
        //musicSource.clip = background;
        //musicSource.Play();
        StartCoroutine("Contador");
    }


// every 2 seconds perform the print()
    IEnumerator Contador()
    {
        for (int i = 0; i < counter.Length; i++)
        {
            SFXSource.Play();
            yield return new WaitForSeconds(2f * Time.deltaTime);
            SFXSource.clip = counter[i];
        }
    }
}
