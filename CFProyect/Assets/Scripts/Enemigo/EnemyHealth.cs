using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; 
    private int currentHealth; 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Movimiento movementScript; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        Debug.Log("Daño recibido: " + damage + ", salud restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        Debug.Log("El enemigo ha muerto.");

        
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.green;
        }

        
        if (movementScript != null)
        {
           // movementScript.target = movementScript.agent.transform; 
            //movementScript.agent.SetDestination(movementScript.target.position);
            movementScript.enabled = false;
            Debug.Log("No se mueve mas");
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}
