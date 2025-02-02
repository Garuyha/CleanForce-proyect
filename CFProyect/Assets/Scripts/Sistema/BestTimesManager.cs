using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BestTimesManager : MonoBehaviour
{
    public static BestTimesManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<float> GetBestTimes()
    {
        string levelKey = $"BestTimes_Level_{GetCurrentLevelIndex()}";
        return LoadBestTimes(levelKey);
    }

    public void ResetBestTimes()
{
    int totalLevels = SceneManager.sceneCountInBuildSettings;

    for (int levelIndex = 0; levelIndex < totalLevels; levelIndex++)
    {
        string levelKey = $"BestTimes_Level_{levelIndex}";

        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.DeleteKey($"{levelKey}_{i}");
        }
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

    public void SaveBestTimes(float newTime)
    {
        string levelKey = $"BestTimes_Level_{GetCurrentLevelIndex()}";

        List<float> bestTimes = LoadBestTimes(levelKey);
        bestTimes.Add(newTime);
        bestTimes.Sort();
        bestTimes = bestTimes.Take(3).ToList();

        for (int i = 0; i < bestTimes.Count; i++)
        {
            PlayerPrefs.SetFloat($"{levelKey}_{i}", bestTimes[i]);
        }

        PlayerPrefs.Save();
    }
}
