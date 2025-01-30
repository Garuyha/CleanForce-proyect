using UnityEngine;
using TMPro; 
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText; 
    public GameObject overlayPanel;    
    private float countdownTime = 3f;    
    private int countSFX;
    private bool countdownActive = true; 
    public GameObject uiPanel;
    [SerializeField] private AudioClip[] counterSFX;

    void Start()
    {
        Time.timeScale = 0f; 
        countSFX = 3;
        overlayPanel.SetActive(true);
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            
            countdownText.text = Mathf.CeilToInt(countdownTime).ToString();
            SoundFXManager.instance.PlaySoundFXClip(counterSFX[countSFX], transform, 1f);
            yield return new WaitForSecondsRealtime(1f); 
            countdownTime -= 1f;
            countSFX--;
        }

        
        SoundFXManager.instance.PlaySoundFXClip(counterSFX[countSFX], transform, 1f);
        countdownText.text = "¡GO!"; 
        yield return new WaitForSecondsRealtime(1f); 
        overlayPanel.SetActive(false);
        countdownText.text = "";
        uiPanel.SetActive(true);
        Time.timeScale = 1f; 
        countdownActive = false;
    }
}
