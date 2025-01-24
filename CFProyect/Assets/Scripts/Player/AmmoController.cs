using UnityEngine;

public class AmmoController : MonoBehaviour
{
    public int maxAmmo = 100; // Cantidad máxima de munición
    public int currentAmmo; // Munición actual
    public float reloadSpeed = 10f; // Velocidad de recarga de munición (cantidad que se recarga por segundo)
    private float lastReloadTime; // Tiempo del último aumento de munición

    void Start()
    {
        
        currentAmmo = maxAmmo;
        lastReloadTime = Time.time; 
    }

    void Update()
    {
        
        if (currentAmmo < maxAmmo && !Input.GetButton("Fire1"))
        {
            ReloadAmmo();
        }
    }

    public bool UseAmmo(int amount)
    {
        if (currentAmmo >= amount)
        {
            currentAmmo -= amount;
            return true; 
        }
        return false; 
    }

    void ReloadAmmo()
    {
        
        if (Time.time - lastReloadTime >= 1f / reloadSpeed) 
        {
            currentAmmo++; 
            lastReloadTime = Time.time; 
            currentAmmo = Mathf.Min(currentAmmo, maxAmmo); 
        }
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}
