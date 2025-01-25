using UnityEngine;

public class ShootingModeSwitcher : MonoBehaviour
{
    public BubbleShooting continuousShooting;  // Referencia al script de disparo continuo
    public ShotgunShooting shotgunShooting;  // Referencia al script de disparo en arco
    private bool isShotgunActive = false;  // Indica si está activo el modo escopeta

    public ObjectPooler bulletPool;  // Referencia al ObjectPooler

    // Valores de lifetime para cada modo
    public float continuousDamping = 3f;
    public float shotgunDamping = 8f;

    void Start()
    {
        ActivateContinuousShooting();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
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
        continuousShooting.enabled = true;
        shotgunShooting.enabled = false;

        // Cambiar el lifetime de las balas de la pool
        bulletPool.SetLinearDampingForAllBullets(continuousDamping);
    }

    void ActivateShotgunShooting()
    {
        isShotgunActive = true;
        continuousShooting.enabled = false;
        shotgunShooting.enabled = true;

        // Cambiar el lifetime de las balas de la pool
        bulletPool.SetLinearDampingForAllBullets(shotgunDamping);
    }
}
