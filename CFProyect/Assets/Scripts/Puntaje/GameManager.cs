using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int nivelMugre = 0;
    [SerializeField] private ObtenerPuntaje puntaje;

    public delegate void OnVictory(); // Evento para manejar la victoria
    public static event OnVictory VictoryEvent;

    private float levelTime = 0f; // Tiempo del nivel en segundos
    private float finalLevelTime = 0f; // Tiempo final que se muestra en la pantalla de victoria
    private bool isLevelActive = true;

    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MugreBase");
        nivelMugre = objects.Length;
    }

    private void Update()
    {
        if (isLevelActive)
        {
            levelTime += Time.deltaTime; // El cronómetro corre mientras el nivel esté activo
        }

        if (nivelMugre == 0) // Condición de victoria
        {
            Victoria();
        }
    }

    public float GetLevelTime()
    {
        return finalLevelTime; // Retorna el tiempo final cuando se detuvo el cronómetro
    }

    public void StopLevelTime()
    {
        isLevelActive = false; // Detener el cronómetro
        finalLevelTime = levelTime; // Guardar el tiempo final cuando se para el nivel
        Debug.Log($"Tiempo final del nivel: {finalLevelTime} segundos");
    }

    public void Victoria()
    {
        StopLevelTime(); // Detener el cronómetro cuando se cumple la victoria
        Time.timeScale = 0f; // Pausar el juego
        VictoryEvent?.Invoke(); // Llamar al evento de victoria
    }
}
