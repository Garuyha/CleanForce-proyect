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
            Destroy(gameObject);
            Debug.Log("B");
        }
    }

}
