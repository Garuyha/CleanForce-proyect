using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int nivelMugre = 0;
    //public List<GameObject> mugreLista;
    private bool noLoop = true;
    [SerializeField] private ObtenerPuntaje puntaje;
    void Start()
    {
        nivelMugreInicial();
    }
    // Update is called once per frame
    void Update()
    {
        if (nivelMugre>0 && noLoop)
        {
            Debug.Log("El nivel de mugre es: " + nivelMugre);

        }else if(nivelMugre<=0 && noLoop)
        {
            Debug.Log("HAS LIMPIADO CON EXITO EL NIVEL");
            puntaje.ObtenerTiempo();
            Debug.Log("Tu tiempo fue: " + ObtenerPuntaje.tiempoSinFiltro);
            noLoop = false;
        }
    }
    public void nivelMugreInicial()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MugreBase");
        int aux = objects.Length;
        nivelMugre = aux;
        for (int i = 0; i < aux; i++)
        {
            //mugreLista.Add(objects[i]);
        }
    }

    public void nivelMugreIncrementar()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Mugre");
        int aux = objects.Length;
        for (int i = 0; i < aux; i++)
        {
            /**if (!mugreLista.Contains(objects[i]))
            {
                mugreLista.Add(objects[i]);
            }**/
        }
    }
}
