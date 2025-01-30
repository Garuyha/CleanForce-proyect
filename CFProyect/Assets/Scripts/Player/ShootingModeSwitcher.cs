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
    [SerializeField] private AudioClip buttonSFX;


    // Definición de los modos de disparo
    public enum ShootingMode
    {
        Continuous,
        Shotgun
    }

    // Evento para notificar cambios de modo
    public delegate void ModeSwitched(ShootingMode newMode);
    public event ModeSwitched OnModeSwitched;

    // Propiedad pública para acceder al modo actual
    public ShootingMode CurrentMode { get; private set; } = ShootingMode.Continuous;

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
                SoundFXManager.instance.PlaySoundFXClip(buttonSFX, transform, 1f);
            }
            else
            {
                ActivateShotgunShooting();
                SoundFXManager.instance.PlaySoundFXClip(buttonSFX, transform, 1f);
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

        // Actualizar el modo actual y emitir evento
        CurrentMode = ShootingMode.Continuous;
        OnModeSwitched?.Invoke(CurrentMode);
    }

    void ActivateShotgunShooting()
    {
        isShotgunActive = true;
        continuousShooting.enabled = false;
        shotgunShooting.enabled = true;

        // Cambiar el lifetime de las balas de la pool
        bulletPool.SetLinearDampingForAllBullets(shotgunDamping);

        // Actualizar el modo actual y emitir evento
        CurrentMode = ShootingMode.Shotgun;
        OnModeSwitched?.Invoke(CurrentMode);
    }
}
