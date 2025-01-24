using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShooting : MonoBehaviour
{
    public Transform firePoint;  // El punto de disparo
    public ObjectPooler bulletPool; //La pool de proyectiles
    public float shootRate = 0.1f;  // La tasa de disparo, menor valor = mayor cadencia
    public float bulletForce = 20f;  // La fuerza con la que se dispara el proyectil
    public float spreadAmount = 5f;  // Cantidad de dispersión (grados)
    private float nextShootTime = 0f;  // Para controlar el tiempo entre disparos


    void Update()
    {
        if (Input.GetButton("Fire1"))  
        {
            if (Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + shootRate;  // Ajusta el tiempo para el siguiente disparo
            }
        }
    }


   void Shoot()
    {
    if(AmmoController.Instance.UseAmmo(1)) // Gasta 1 unidad de munición por disparo
        { 
    
        GameObject bullet = bulletPool.GetPoolObject();  
        bullet.transform.position = firePoint.transform.position;
        bullet.SetActive(true);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // rb.linearVelocity = Vector2.zero;

        // Dispersión
        float randomAngle = Random.Range(-spreadAmount, spreadAmount);  // Ángulo aleatorio para dispersión
        Vector3 direction = firePoint.up;  // Dirección original
        direction = Quaternion.Euler(0, 0, randomAngle) * direction;  // Rotación

        // Aplicar la fuerza con la dirección dispersada
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);  // Aplica la fuerza
        }
    }

}
    




