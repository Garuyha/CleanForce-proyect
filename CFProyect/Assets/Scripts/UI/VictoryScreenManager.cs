using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryScreenManager : MonoBehaviour
{
    public GameObject victoryPanel; // Panel de victoria
    public TextMeshProUGUI timeText; // Texto para mostrar el tiempo de finalización (TextMeshPro)
    public TextMeshProUGUI timeTextFondo; // Texto de fondo (para darle un efecto similar a un contorno)
    private GameManager gameManager; // Referencia al GameManager

    void Start()
    {
        // Encontrar el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        // Suscribirse al evento de victoria
        GameManager.VictoryEvent += ShowVictoryPanel;
    }

    private void OnDisable()
    {
        // Desuscribirse del evento de victoria cuando el script se desactive
        GameManager.VictoryEvent -= ShowVictoryPanel;
    }

    private void ShowVictoryPanel()
    {
        // Activar el panel de victoria
        victoryPanel.SetActive(true);

        // Obtener el tiempo final del nivel desde el GameManager
        float timeElapsed = gameManager.GetLevelTime();
        string formattedTime = FormatTime(timeElapsed);

        // Establecer el texto del tiempo
        timeText.text = formattedTime;
        timeTextFondo.text = formattedTime;

        // Forzar la actualización de los textos para asegurarse de que se rendericen correctamente
        timeText.ForceMeshUpdate();
        timeTextFondo.ForceMeshUpdate();
    }

    // Método para formatear el tiempo en minutos, segundos y milisegundos
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f);

        return $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    // Método para cargar el siguiente nivel
    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Restaurar el tiempo normal
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Cargar el siguiente nivel
        }
        else
        {
            Debug.Log("¡No hay más niveles! Volviendo al menú principal.");
            SceneManager.LoadScene("MainMenu"); // Volver al menú principal si no hay más niveles
        }
    }

    // Método para regresar al menú principal
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Restaurar el tiempo normal
        SceneManager.LoadScene("MainMenu"); // Cargar la escena del menú principal
    }
}
