using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class GameManager : MonoBehaviour
{
    public static int nivelMugre = 0;
    [SerializeField] private ObtenerPuntaje puntaje;

    public delegate void OnVictory();
    public static event OnVictory VictoryEvent;

    private bool victoryTriggered = false;
    private float levelTime;
    private float finalLevelTime = 0f;
    private bool isLevelActive = false;

    void OnEnable()
    {
        levelTime = 0f;
        isLevelActive = true;
    }

    private void Update()
    {
        if (isLevelActive)
        {
            levelTime += Time.deltaTime;
        }

        if (nivelMugre == 0 && !victoryTriggered)
        {
            Victoria();
        }
    }

    public float GetLevelTime()
    {
        return finalLevelTime;
    }

    public void StopLevelTime()
    {
        isLevelActive = false;
        finalLevelTime = levelTime;
    }

    public void Victoria()
    {
        victoryTriggered = true;
        StopLevelTime();
        SaveBestTimes(finalLevelTime); // Guardar los mejores tiempos
        VictoryEvent?.Invoke();
    }

    private void SaveBestTimes(float newTime)
    {
        string levelKey = $"BestTimes_Level_{GetCurrentLevelIndex()}";

        // Obtener los tiempos guardados
        List<float> bestTimes = LoadBestTimes(levelKey);

        // Añadir el nuevo tiempo, ordenar y guardar los 3 mejores
        bestTimes.Add(newTime);
        bestTimes.Sort();
        bestTimes = bestTimes.Take(3).ToList();

        // Guardar los mejores tiempos
        for (int i = 0; i < bestTimes.Count; i++)
        {
            PlayerPrefs.SetFloat($"{levelKey}_{i}", bestTimes[i]);
        }

        PlayerPrefs.Save();
    }

    private List<float> LoadBestTimes(string levelKey)
    {
        List<float> bestTimes = new List<float>();

        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.HasKey($"{levelKey}_{i}"))
            {
                bestTimes.Add(PlayerPrefs.GetFloat($"{levelKey}_{i}"));
            }
        }

        return bestTimes;
    }

    private int GetCurrentLevelIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public List<float> GetBestTimes()
    {
        string levelKey = $"BestTimes_Level_{GetCurrentLevelIndex()}";
        return LoadBestTimes(levelKey);
    }

    public void ResetBestTimes()
    {
        string levelKey = $"BestTimes_Level_{GetCurrentLevelIndex()}";

        // Eliminar los tiempos de ese nivel
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.DeleteKey($"{levelKey}_{i}");
        }

        PlayerPrefs.Save();
    }
}

