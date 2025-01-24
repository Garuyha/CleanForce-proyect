using UnityEngine;

public class ShootingModeSwitcher : MonoBehaviour
{
    public BubbleShooting continuousShooting;  // Referencia al script de disparo continuo
    public ShotgunShooting shotgunShooting;  // Referencia al script de disparo en arco
    private bool isShotgunActive = false;  // Indica si está activo el modo escopeta

    // Valores de lifetime para cada modo
    public float continuousLifetime = 0.8f;  // Tiempo de vida para el modo continuo
    public float shotgunLifetime = 0.1f;     // Tiempo de vida para el modo escopeta

    // Parámetro de dispersión para el modo escopeta
    public float continuousShotgunSpread = 10f;

    void Start()
    {
        // Asegurarnos de que el disparo continuo esté activo por defecto
        ActivateContinuousShooting();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))  // Cambiar modo al presionar "Q"
        {
            if (isShotgunActive)
            {
                ActivateContinuousShooting();
            }
            else
            {
                ActivateShotgunShooting();
            }
        }
    }

    void ActivateContinuousShooting()
    {
        isShotgunActive = false;
        continuousShooting.enabled = true;  // Activar el disparo continuo
        shotgunShooting.enabled = false;  // Desactivar el disparo en arco

        // Cambiar el lifetime de las balas activas a `continuousLifetime`
        SetBulletLifetime(continuousLifetime);
    }

    void ActivateShotgunShooting()
    {
        isShotgunActive = true;
        continuousShooting.enabled = false;  // Desactivar el disparo continuo
        shotgunShooting.enabled = true;  // Activar el disparo en arco

        // Cambiar el lifetime de las balas activas a `shotgunLifetime`
        SetBulletLifetime(shotgunLifetime);

        // Actualizar el valor de dispersión para el disparo en modo escopeta
        shotgunShooting.shotgunSpreadAmount = continuousShotgunSpread;
    }

    // Método para cambiar el lifetime de todas las balas activas
    void SetBulletLifetime(float newLifetime)
    {
        Bullet[] bullets = FindObjectsOfType<Bullet>();  // Encontrar todas las balas activas
        foreach (Bullet bullet in bullets)
        {
            if (bullet != null)
            {
                bullet.SetLifetime(newLifetime);  // Cambiar el lifetime de cada bala
            }
        }
    }
}
