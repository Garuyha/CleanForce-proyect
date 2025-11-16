using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static int nivelMugre = 0;
    [SerializeField] private ObtenerPuntaje puntaje;
    [SerializeField] private TMP_Text textPuntaje;

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
        textPuntaje.text = "Mugre: "+nivelMugre.ToString();
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
        BestTimesManager.Instance.SaveBestTimes(finalLevelTime);
        VictoryEvent?.Invoke();
        VictoryScreenManager.Instance.ShowVictoryPanel();
    }


    private int GetCurrentLevelIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

   
}

