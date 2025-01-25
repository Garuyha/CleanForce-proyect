using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; 
    private int currentHealth; 
    private SpriteRenderer spriteRenderer; 
    private MonoBehaviour movementScript; 

    void Start()
    {
        currentHealth = maxHealth; 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        movementScript = GetComponent<ExampleClass>(); 
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
            spriteRenderer.color = Color.blue;
        }

        
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}
