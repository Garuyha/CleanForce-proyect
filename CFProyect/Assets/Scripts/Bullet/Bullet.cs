using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    private Coroutine lifetimeCoroutine;
    private Rigidbody2D rb;

    public int damage = 20; 

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        lifetimeCoroutine = StartCoroutine(HandleLifetime());
    }

    void OnDisable()
    {
        if (lifetimeCoroutine != null)
        {
            StopCoroutine(lifetimeCoroutine);
            lifetimeCoroutine = null;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  
            }
        }

        
        gameObject.SetActive(false);
    }

    
    private IEnumerator HandleLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }

    
    public void SetLinearDamping(float newLinearDamping)
    {
        rb.linearDamping = newLinearDamping;
    }
}
