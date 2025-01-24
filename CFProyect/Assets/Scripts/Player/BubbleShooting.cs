using UnityEngine;

public class BubbleShooter : MonoBehaviour
{
    public Transform firePoint; // Punto desde donde se disparan las burbujas
    public GameObject bubblePrefab; // Prefab de las burbujas
    public float bubbleForce = 20f; // Fuerza aplicada a las burbujas
    public float fireRate = 0.1f; // Tiempo entre burbujas (velocidad del chorro)
    public float bubbleLifetime = 2f; // Tiempo antes de que las burbujas se destruyan
    public float spreadAmount = 0.1f; // Rango de dispersión en el disparo

    private AmmoController ammoController; // Referencia al controlador de munición
    private float nextFireTime = 0f; // Tiempo para controlar el disparo continuo

    void Start()
    {
        // Obtener la referencia al AmmoController
        ammoController = GetComponent<AmmoController>();
    }

    void Update()
    {
        // Mantener el disparo constante mientras se mantiene el botón de disparo
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && ammoController.GetCurrentAmmo() > 0)
        {
            nextFireTime = Time.time + fireRate; // Control de la tasa de disparo
            if (ammoController.UseAmmo(1)) // Gastar 1 de munición por disparo
            {
                ShootBubble();
            }
        }
    }

    void ShootBubble()
    {
        // Crear una pequeña variación en el punto de disparo
        Vector3 spreadOffset = new Vector3(Random.Range(-spreadAmount, spreadAmount), Random.Range(-spreadAmount, spreadAmount), 0);
        Vector3 spawnPosition = firePoint.position + spreadOffset;

        // Crear una burbuja en el punto con variación
        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, firePoint.rotation);

        // Aplicar fuerza a la burbuja para que se mueva hacia adelante
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bubbleForce, ForceMode2D.Impulse);

        // Destruir la burbuja automáticamente después del tiempo definido
        Destroy(bubble, bubbleLifetime);
    }
}
