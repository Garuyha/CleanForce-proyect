using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public Slider ammoSlider;  
    private AmmoController ammoController;  

    void Start()
    {
        ammoController = GameObject.Find("Player").GetComponent<AmmoController>();
        ammoSlider.maxValue = ammoController.maxAmmo;
        ammoSlider.value = ammoController.currentAmmo;
    }

    void Update()
    {

        ammoSlider.value = ammoController.currentAmmo;
    }
}
