using UnityEngine;

public class MugrePadre: MonoBehaviour
{
    [SerializeField] public int maxHealth = 40;
    [SerializeField] public int currentHealth;
    [SerializeField] private GameManager mugreManager;

    private void Awake()
    {
        currentHealth = maxHealth;
        GameObject.Find("GameManager");
    }

    public void TakeDmg(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            GameManager.nivelMugre--;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            if (this.gameObject.tag == "Mugre")
                EnemigoPadre.mugreActual--;

            Destroy(gameObject);
        }
    }

}
