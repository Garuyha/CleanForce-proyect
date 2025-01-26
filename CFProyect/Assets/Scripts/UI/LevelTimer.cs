using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    private float elapsedTime = 0f;  
    private bool isRunning = true;   

    void Update()
    {
        if (isRunning)
        {
            
            elapsedTime += Time.deltaTime;

            
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 100f) % 100f); 

            
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
    }

    
    public void StopTimer()
    {
        isRunning = false;
    }

    
    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

}
