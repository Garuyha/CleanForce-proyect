using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public Slider ammoSlider;  
    public AmmoController ammoController;  

    void Start()
    {

        ammoSlider.maxValue = ammoController.maxAmmo;
        ammoSlider.value = ammoController.currentAmmo;
    }

    void Update()
    {

        ammoSlider.value = ammoController.currentAmmo;
    }
}
