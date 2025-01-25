using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    private Coroutine lifetimeCoroutine;
    private Rigidbody2D rb;

    public int damage = 5;
    public int dmgMugre = 20;

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
            EnemigoPadre enemyHealth = collision.gameObject.GetComponent<EnemigoPadre>();
            if (enemyHealth != null)
            {
                enemyHealth.DealDamage(damage);  
            }
        }
        else if (collision.gameObject.CompareTag("Blanco"))
        {
            MugrePadre saludMugre = collision.gameObject.GetComponent<MugrePadre>();
            if(saludMugre != null)
            {
                Debug.Log("A");
                saludMugre.TakeDmg(dmgMugre);
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
