using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class VictoryScreenManager : MonoBehaviour
{
    public GameObject victoryPanel; // Panel de victoria
    public TextMeshProUGUI[] bestTimesTexts; // Arreglo de TextMeshPro para los mejores tiempos (pares: texto principal y fondo)
    public TextMeshProUGUI currentTimeText; // Texto para mostrar el tiempo del nivel actual (Texto principal)
    public TextMeshProUGUI currentTimeTextFondo; // Texto de fondo para el tiempo del nivel actual
    public AudioClip victorySound; // Pista de audio para la victoria
    private AudioSource audioSource; // Fuente de audio

    public List<float> defaultTimes = new List<float> { 90f, 120f, 150f }; // Tiempos predeterminados
    private GameManager gameManager; // Referencia al GameManager

    private bool isRainbowEffectActive = false;

    void Start()
    {
        // Obtener referencias
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();

        // Verificar AudioSource
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

        // Mostrar el tiempo del nivel actual
        float currentLevelTime = gameManager.GetLevelTime();
        string formattedCurrentTime = FormatTime(currentLevelTime);
        currentTimeText.text = formattedCurrentTime;
        currentTimeTextFondo.text = formattedCurrentTime;

        // Obtener los 3 mejores tiempos
        List<float> bestTimes = gameManager.GetBestTimes();

        // Si no hay suficientes registros, completar con el ranking por defecto
        if (bestTimes.Count < 3)
        {
            foreach (float defaultTime in defaultTimes)
            {
                if (bestTimes.Count >= 3) break;
                bestTimes.Add(defaultTime);
            }

            // Ordenar los tiempos por si mezclamos predeterminados con reales
            bestTimes.Sort();
        }

        // Mostrar los mejores tiempos en los TextMeshPro y resaltar si el jugador batió un tiempo
        for (int i = 0; i < bestTimesTexts.Length / 2; i++) // Iterar solo 3 tiempos (pares: texto + fondo)
        {
            if (i < bestTimes.Count)
            {
                string formattedTime = FormatTime(bestTimes[i]);
                bestTimesTexts[i * 2].text = formattedTime;       // Texto principal
                bestTimesTexts[i * 2 + 1].text = formattedTime;   // Fondo del texto

                // Si el jugador batió este tiempo, activar el efecto de arcoíris
                if (Mathf.Approximately(bestTimes[i], currentLevelTime))
                {
                    StartRainbowEffect(bestTimesTexts[i * 2]);
                }
            }
            else
            {
                // Si por alguna razón no hay suficientes tiempos, dejar el texto vacío
                bestTimesTexts[i * 2].text = "--:--:--";
                bestTimesTexts[i * 2 + 1].text = "--:--:--";
            }
        }

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

    private void StartRainbowEffect(TextMeshProUGUI textMesh)
    {
        if (isRainbowEffectActive) return; // Evitar múltiples efectos simultáneamente

        isRainbowEffectActive = true;
        StartCoroutine(RainbowEffectCoroutine(textMesh));
    }

    private System.Collections.IEnumerator RainbowEffectCoroutine(TextMeshProUGUI textMesh)
    {
        float duration = 3f; // Duración del efecto en segundos
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Crear un gradiente de arcoíris
            var gradient = new VertexGradient(
                Color.HSVToRGB((elapsed / duration) % 1f, 1f, 1f),
                Color.HSVToRGB((elapsed / duration + 0.33f) % 1f, 1f, 1f),
                Color.HSVToRGB((elapsed / duration + 0.66f) % 1f, 1f, 1f),
                Color.HSVToRGB((elapsed / duration + 1f) % 1f, 1f, 1f)
            );

            textMesh.colorGradient = gradient;

            yield return null; // Esperar al siguiente frame
        }

        isRainbowEffectActive = false;
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
