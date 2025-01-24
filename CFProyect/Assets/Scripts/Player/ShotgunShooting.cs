using UnityEngine;

public class ShotgunShooting : MonoBehaviour
{
    public Transform[] firePoints;  // Array de puntos de disparo
    public ObjectPooler bulletPool;  // Pool de balas
    public float bulletForce = 20f;  // Fuerza con la que se dispara el proyectil
    public float shotgunSpreadAmount = 10f;  // Cantidad de dispersión (en grados)
    public float shootRate = 0.1f;  // Tasa de disparo
    private float nextShootTime = 0f;  // Para controlar el tiempo entre disparos

    void Update()
    {
        if (Time.time >= nextShootTime && Input.GetButton("Fire1"))  // Mientras se mantenga presionado
        {
            Shoot();
            nextShootTime = Time.time + shootRate;  // Ajusta el tiempo para el siguiente disparo
        }
    }

    void Shoot()
    {
        foreach (Transform firePoint in firePoints)  // Recorrer todos los puntos de disparo
        {
            GameObject bullet = bulletPool.GetPoolObject();  // Obtener un proyectil de la pool
            bullet.transform.position = firePoint.position;
            bullet.SetActive(true);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Aplicar dispersión
            float randomAngle = Random.Range(-shotgunSpreadAmount, shotgunSpreadAmount);  // Ángulo aleatorio para dispersión
            Vector3 direction = firePoint.up;  // Dirección original del firePoint
            direction = Quaternion.Euler(0, 0, randomAngle) * direction;  // Rotar la dirección por el ángulo aleatorio

            // Aplicar la fuerza con la dirección dispersada
            rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);  // Aplica la fuerza
        }
    }
}
