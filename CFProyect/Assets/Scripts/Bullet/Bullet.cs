using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    private float timeAlive;
    private Coroutine lifetimeCoroutine;

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
    


   private void OnCollisionEnter2D (Collision2D collision)  
   {
        gameObject.SetActive(false);
   }

   private IEnumerator HandleLifetime() //Espera el tiempo de vida y desactiva el proyectil si no colisiona con nada
    {
        yield return new WaitForSeconds(lifetime);

        gameObject.SetActive(false);
    }
}
