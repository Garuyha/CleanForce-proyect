using System.Collections.Generic;
using UnityEngine;

public class ShotgunShooting : MonoBehaviour
{
    public List<Transform> firePoints; // Lista de puntos de disparo configurados manualmente
    public ObjectPooler bulletPool; // Pool de proyectiles
    public float shootRate = 0.2f; // Tasa de disparo
    public float bulletForce = 20f; // Fuerza de los proyectiles
    public float spreadAmount = 15f; // Cantidad de dispersión (grados alrededor del FirePoint)
    public int ammoPerShot = 1; // Munición consumida por disparo

    private float nextShootTime = 0f; // Control del tiempo entre disparos

    void Update()
    {
        if (Input.GetButton("Fire1")) // Mientras se mantenga presionado
        {
            if (Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + shootRate; // Ajusta el tiempo para el siguiente disparo
            }
        }
    }

    void Shoot()
    {
        // Verificar si hay suficiente munición para disparar
        if (!AmmoController.Instance.UseAmmo(ammoPerShot))
        {
            return; // Si no hay suficiente munición, no disparamos
        }

        foreach (Transform firePoint in firePoints)
        {
            GameObject bullet = bulletPool.GetPoolObject(); // Obtener una bala de la pool
            if (bullet != null) // Verificar que la pool devuelve un objeto
            {
                bullet.transform.position = firePoint.position; // Posicionar la bala en el FirePoint
                bullet.transform.rotation = firePoint.rotation; // Rotación inicial basada en el FirePoint
                bullet.SetActive(true);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                // Añadir dispersión al disparo
                float randomAngle = Random.Range(-spreadAmount, spreadAmount); // Generar dispersión aleatoria
                Vector3 direction = Quaternion.Euler(0, 0, randomAngle) * firePoint.up; // Rotar la dirección inicial

                // Aplicar fuerza al proyectil
                rb.linearVelocity = Vector2.zero; // Reiniciar la velocidad residual
                rb.AddForce(direction * bulletForce, ForceMode2D.Impulse); // Disparar la bala
            }
        }
    }
}
