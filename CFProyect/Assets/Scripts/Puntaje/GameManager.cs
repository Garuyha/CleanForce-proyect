using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int nivelMugre = 0;
    [SerializeField] private ObtenerPuntaje puntaje;

    public delegate void OnVictory(); 
    public static event OnVictory VictoryEvent;

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

        if (nivelMugre == 0) 
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
        StopLevelTime(); 
        Time.timeScale = 0f; 
        VictoryEvent?.Invoke(); 
    }
}
