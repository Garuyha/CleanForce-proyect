using UnityEngine;

public class MugrePadre: MonoBehaviour
{
    [SerializeField] public int maxHealth = 40;
    [SerializeField] public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDmg(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            GameManager.nivelMugre--;
            if(this.gameObject.tag == "Mugre")
                EnemigoPadre.mugreActual--;
            Destroy(gameObject);
            Debug.Log("El nivel de mugre actual es: " + GameManager.nivelMugre);
        }
    }

}
