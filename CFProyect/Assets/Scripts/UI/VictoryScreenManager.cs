using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryScreenManager : MonoBehaviour
{
    public GameObject victoryPanel; // Panel de victoria
    public TextMeshProUGUI timeText; // Texto para mostrar el tiempo de finalización (TextMeshPro)
    public TextMeshProUGUI timeTextFondo;
    public AudioClip victorySound; // Pista de audio para la victoria
    private AudioSource audioSource; // Fuente de audio
    private GameManager gameManager; // Referencia al GameManager

    void Start()
    {
        // Obtener referencias
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();

        // Asegurarte de que el AudioSource esté configurado correctamente
        if (audioSource == null)
        {
            Debug.LogError("No se encontró un AudioSource en el GameObject. Por favor, agrégalo.");
        }
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
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);

        // Obtener el tiempo final del nivel desde el GameManager
        float timeElapsed = gameManager.GetLevelTime();
        string formattedTime = FormatTime(timeElapsed);
        timeText.text = formattedTime;
        timeTextFondo.text = formattedTime;

        // Reproducir la música de victoria una vez
        if (audioSource != null && victorySound != null)
        {
            audioSource.PlayOneShot(victorySound);
        }
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
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Restaurar el tiempo normal
        SceneManager.LoadScene("MainMenu"); // Cargar la escena del menú principal
    }
}
