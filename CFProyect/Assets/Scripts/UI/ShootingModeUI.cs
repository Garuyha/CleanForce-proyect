using UnityEngine;
using UnityEngine.UI;

public class ShootingModeUI : MonoBehaviour
{
    public ShootingModeSwitcher shootingModeSwitcher; // Referencia al script de cambio de modo
    public Image continuousIcon;  // Ícono del modo continuo
    public Image shotgunIcon;     // Ícono del modo escopeta
    public Color activeColor = Color.white;     // Color del modo activo
    public Color inactiveColor = Color.gray;   // Color del modo inactivo

    void OnEnable()
    {
        if (shootingModeSwitcher != null)
        {
            shootingModeSwitcher.OnModeSwitched += UpdateIcons;
        }

        // Actualizar íconos al activarse este script
        UpdateIcons(shootingModeSwitcher.CurrentMode);
    }

    void OnDisable()
    {
        if (shootingModeSwitcher != null)
        {
            shootingModeSwitcher.OnModeSwitched -= UpdateIcons;
        }
    }

    private void UpdateIcons(ShootingModeSwitcher.ShootingMode currentMode)
    {
        if (currentMode == ShootingModeSwitcher.ShootingMode.Continuous)
        {
            continuousIcon.color = activeColor;
            shotgunIcon.color = inactiveColor;
        }
        else if (currentMode == ShootingModeSwitcher.ShootingMode.Shotgun)
        {
            continuousIcon.color = inactiveColor;
            shotgunIcon.color = activeColor;
        }
    }
}
