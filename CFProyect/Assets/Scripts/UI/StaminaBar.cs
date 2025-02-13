using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;  
    private PlayerMovement playerMovement; 

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        staminaSlider.maxValue = playerMovement.maxStamina;
        staminaSlider.value = playerMovement.stamina;
    }

    void Update()
    {
        
        staminaSlider.value = playerMovement.stamina;
        if (playerMovement.stamina < playerMovement.staminaLowThreshold)
        {
        staminaSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
        else
        {
        staminaSlider.fillRect.GetComponent<Image>().color = new Color32(153, 229, 80, 255);
        }
    }
}
