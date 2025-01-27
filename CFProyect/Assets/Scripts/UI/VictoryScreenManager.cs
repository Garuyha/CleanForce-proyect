using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Collections; 
using System;

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
    private bool newBestTimeAchieved = false; // Indicador para verificar si un nuevo mejor tiempo fue alcanzado

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
        Time.timeScale = 0f; // Pausar el juego
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

        // Verificar si el jugador ha batido algún tiempo en el ranking
        newBestTimeAchieved = false;

        // Mostrar los mejores tiempos en los TextMeshPro
        for (int i = 0; i < bestTimesTexts.Length / 2; i++) // Iterar solo 3 tiempos (pares: texto + fondo)
        {
            if (i < bestTimes.Count)
            {
                string formattedTime = FormatTime(bestTimes[i]);
                bestTimesTexts[i * 2].text = formattedTime;       // Texto principal
                bestTimesTexts[i * 2 + 1].text = formattedTime;   // Fondo del texto

                // Si el tiempo actual del jugador es igual a este tiempo (batió este tiempo)
                if (Mathf.Approximately(bestTimes[i], gameManager.GetLevelTime()) && !newBestTimeAchieved)
                {
                    newBestTimeAchieved = true; // Solo aplicar el arcoíris al primer nuevo tiempo batido
                    ApplyRainbowEffectToText(bestTimesTexts[i * 2]); // Aplicar el efecto arcoíris
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

    private void ApplyRainbowEffectToText(TextMeshProUGUI textMesh)
    {
        // Llamamos a la corutina para aplicar el efecto arcoíris
        StartCoroutine(RainbowEffectCoroutine(textMesh));
    }

    private IEnumerator RainbowEffectCoroutine(TextMeshProUGUI textMesh)
{
    // Mientras la pantalla de victoria esté activa, actualizar el gradiente de forma continua
    while (victoryPanel.activeSelf)
    {
        var rainbowTime = Mathf.PingPong(Time.unscaledTime, 1f); // Oscilar entre 0 y 1 para el gradiente

        // Crear un gradiente arcoíris con la transición de colores
        var gradient = new VertexGradient(
            Color.HSVToRGB(rainbowTime, 1f, 1f),
            Color.HSVToRGB((rainbowTime + 0.33f) % 1f, 1f, 1f),
            Color.HSVToRGB((rainbowTime + 0.66f) % 1f, 1f, 1f),
            Color.HSVToRGB((rainbowTime + 1f) % 1f, 1f, 1f)
        );

        // Aplicar el gradiente arcoíris solo al texto principal (no al fondo)
        textMesh.colorGradient = gradient;

        yield return null; // Esperar al siguiente frame
    }

    // Limpiar el gradiente cuando se cambia de escena (usamos un gradiente con colores transparentes)
    textMesh.colorGradient = new VertexGradient(Color.clear, Color.clear, Color.clear, Color.clear);
}


    private void Update()
    {
        // Solo aplicar el efecto arcoíris si el panel de victoria está activo
        if (victoryPanel.activeSelf && newBestTimeAchieved)
        {
            int newBestTimeIndex = GetNewBestTimeIndex();

            // Verificar que el índice obtenido es válido
            if (newBestTimeIndex >= 0 && newBestTimeIndex < bestTimesTexts.Length)
            {
                // Continuar el efecto arcoíris si el nuevo mejor tiempo fue alcanzado
                ApplyRainbowEffectToText(bestTimesTexts[newBestTimeIndex]);
            }
        }
    }

    private int GetNewBestTimeIndex()
    {
        // Obtener el tiempo del jugador como un número de tipo float
        float currentPlayerTime = gameManager.GetLevelTime();

        for (int i = 0; i < bestTimesTexts.Length / 2; i++) // Iteramos solo hasta la mitad del array (3 tiempos)
        {
            // Intentar obtener el tiempo formateado de la lista de mejores tiempos
            string timeString = bestTimesTexts[i * 2].text;
            float bestTime = ParseTimeString(timeString);

            // Comprobar si el tiempo del jugador coincide con este tiempo
            if (Mathf.Approximately(currentPlayerTime, bestTime))
            {
                return i * 2; // Retornar el índice del texto principal correspondiente
            }
        }

        // Si no encontramos el tiempo, retornamos -1
        return -1;
    }

    private float ParseTimeString(string timeString)
    {
        string[] timeParts = timeString.Split(':');
        if (timeParts.Length == 3)
        {
            try
            {
                // Convertir minutos, segundos y milisegundos a un tiempo en segundos
                float minutes = float.Parse(timeParts[0]);
                float seconds = float.Parse(timeParts[1]);
                float milliseconds = float.Parse(timeParts[2]);

                return minutes * 60f + seconds + milliseconds / 100f; // Retornar el tiempo total en segundos
            }
            catch (FormatException e)
            {
                Debug.LogError($"Error de formato en el tiempo: {timeString}. Detalles: {e.Message}");
                return float.MaxValue;
            }
        }

        // Si el formato no es válido, retornar un valor por defecto (muy alto)
        return float.MaxValue;
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
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetRanking()
    {
        gameManager.ResetBestTimes();
        Debug.Log("Tiempos reiniciados!");
    }
}









