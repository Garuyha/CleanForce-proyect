using UnityEngine;

public class MugrePadre: MonoBehaviour
{
    [SerializeField] public int maxHealth = 40;
    [SerializeField] public int currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        GameManager.nivelMugre++;
    }
    private void OnDisable()
    {
        GameManager.nivelMugre--; 
        if (this.gameObject.tag == "Mugre")
            EnemigoPadre.mugreActual--;
        Debug.Log("El nivel de mugre es: " + GameManager.nivelMugre);
    }
    public void TakeDmg(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
