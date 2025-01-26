using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryScreenManager : MonoBehaviour
{
    public GameObject victoryPanel;
    public TextMeshProUGUI timeText; 
    public TextMeshProUGUI timeTextFondo; 
    private GameManager gameManager; 

    void Start()
    {
        
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
       
        GameManager.VictoryEvent += ShowVictoryPanel;
    }

    private void OnDisable()
    {
        
        GameManager.VictoryEvent -= ShowVictoryPanel;
    }

    private void ShowVictoryPanel()
    {
        
        victoryPanel.SetActive(true);

        
        float timeElapsed = gameManager.GetLevelTime();
        string formattedTime = FormatTime(timeElapsed);

        
        timeText.text = formattedTime;
        timeTextFondo.text = formattedTime;

    }

    
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f);

        return $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    
    public void LoadNextLevel()
    {
        Time.timeScale = 1f; 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); 
        }
        else
        {
            Debug.Log("¡No hay más niveles! Volviendo al menú principal.");
            SceneManager.LoadScene("MainMenu"); // Volver al menú principal si no hay más niveles
        }
    }

    
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }
}
