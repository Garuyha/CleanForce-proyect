using UnityEngine;

public class AmmoController : MonoBehaviour
{
    public static AmmoController Instance; // Instancia estática para acceder desde otros scripts
    public int maxAmmo = 100;  // Máxima cantidad de munición
    public int currentAmmo;    // Munición actual
    public float reloadSpeed = 10f;  // Velocidad de recarga
    private float lastReloadTime;  // Tiempo de la última recarga

    void Awake()
    {
        // Asegurarnos de que solo haya una instancia de AmmoController
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Inicializar la munición
        currentAmmo = maxAmmo;
        lastReloadTime = Time.time; // El contador de recarga empieza
    }

    void Update()
    {
        // Solo recargar si la munición no está al máximo y no está disparando
        if (currentAmmo < maxAmmo && !Input.GetButton("Fire1"))
        {
            ReloadAmmo();
        }
    }

    // Intentar usar una cantidad de munición
    public bool UseAmmo(int amount)
    {
        if (currentAmmo >= amount)
        {
            currentAmmo -= amount;
            return true;
        }
        return false;
    }

    // Recargar munición
    void ReloadAmmo()
    {
        if (Time.time - lastReloadTime >= 1f / reloadSpeed) // Recarga en intervalos
        {
            currentAmmo++; // Recargar una unidad
            lastReloadTime = Time.time; // Actualizar el tiempo de la última recarga
            currentAmmo = Mathf.Min(currentAmmo, maxAmmo); // No exceder el máximo
        }
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}
