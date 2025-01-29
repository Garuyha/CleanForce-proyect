using System.Collections.Generic;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Globalization;

public class ObtenerPuntaje : MonoBehaviour
{
    [SerializeField] private TMP_Text tiempo;
    [SerializeField] public static float tiempoFloat;
    [SerializeField] public static string tiempoString, tiempoSinFiltro;
    private  List<char> charsToRemove = new List<char>() { ':' };

    public void ObtenerTiempo()
    {
        tiempoSinFiltro = tiempo.text;
        tiempoString = tiempo.text;
        tiempoString = Filter(tiempoString, charsToRemove);
        tiempoFloat = float.Parse(tiempoString, CultureInfo.InvariantCulture.NumberFormat);
    }
    private string Filter(string str, List<char> charsToRemove)
    {
        foreach (char c in charsToRemove)
        {
            str = str.Replace(c.ToString(), String.Empty);
        }

        return str;
    }
}
