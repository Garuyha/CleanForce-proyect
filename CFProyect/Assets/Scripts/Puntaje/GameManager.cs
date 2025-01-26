using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int nivelMugre = 0;
    private bool noLoop = true;
    [SerializeField] private ObtenerPuntaje puntaje;
    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MugreBase");
        nivelMugre = objects.Length;
    }
    private void Update()
    {
        if (nivelMugre == 0)
            Victoria();
    }
    public void Victoria()
    {
        Debug.Log("HAS LIMPIADO CON EXITO EL NIVEL");
        puntaje.ObtenerTiempo();
        Debug.Log("Tu tiempo fue: " + ObtenerPuntaje.tiempoSinFiltro);

    }
}
