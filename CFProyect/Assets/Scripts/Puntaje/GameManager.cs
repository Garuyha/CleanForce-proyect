using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int nivelMugre = 0;
    private bool noLoop = true;
    [SerializeField] private ObtenerPuntaje puntaje;
    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MugreBase");
        nivelMugre = objects.Length;
        Debug.Log("El nivel de mugre inicial es: " + nivelMugre);
    }

    // Update is called once per frame
    void Update()
    {
        if (nivelMugre <= 0 && noLoop)
        {
            Debug.Log("HAS LIMPIADO CON EXITO EL NIVEL");
            puntaje.ObtenerTiempo();
            Debug.Log("Tu tiempo fue: " + ObtenerPuntaje.tiempoSinFiltro);
            noLoop = false;
        }
    }
}
