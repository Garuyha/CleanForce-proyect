using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    private Coroutine lifetimeCoroutine;
    private Rigidbody2D rb;


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
        gameObject.SetActive(false);
    }

    private IEnumerator HandleLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }

    // Método para actualizar el tiempo de vida de las balas
    public void SetLinearDamping(float newLinearDamping)
    {
        rb.linearDamping = newLinearDamping;
    }
}
