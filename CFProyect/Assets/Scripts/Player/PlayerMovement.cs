using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad básica de movimiento
    public float sprintSpeed = 8f; // Velocidad de correr
    public float tiredSpeed = 2f; // Velocidad cuando está sin stamina
    public float maxStamina = 100f; // Cantidad máxima de stamina
    public float stamina = 100f; // Stamina actual
    public float staminaDrainRate = 10f; // Cuánto consume de stamina al correr por segundo
    public float staminaRegenRate = 5f; // Cuánto se regenera de stamina por segundo cuando no está corriendo
    public float staminaLowThreshold = 30f; // Umbral de stamina baja
    private bool isTired;
    private bool isRunning;

    public Rigidbody2D rb;
    public Camera cam;

    private Vector2 movement;
    private Vector2 mousePos;

    private bool canSprint = true; // Para verificar si puede correr o no

    void Update()
    {
        // Leer entrada del teclado
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Leer la posición del mouse en coordenadas del mundo
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Comprobar si se está presionando Shift para correr y si tiene stamina
        if(stamina > staminaLowThreshold)
        {
            isTired = false;
            canSprint = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && canSprint)
        {
            // Consumir stamina al correr
            stamina -= staminaDrainRate * Time.deltaTime;
            if (stamina < 0) stamina = 0; // Evitar que la stamina sea negativa
        }
        else
        {
            // Regenerar stamina cuando no se está corriendo
            if (stamina < maxStamina)
            {
                stamina += staminaRegenRate * Time.deltaTime;
                if (stamina > maxStamina) stamina = maxStamina; // Limitar la stamina al máximo
            }

        }
    }

    void FixedUpdate()
    {
        // Determinar la velocidad de movimiento
        float currentSpeed;

        // Si se ha agotado la stamina, el jugador se mueve a la velocidad de cansancio
        if (stamina <= 0)
        {
            isTired = true;
            canSprint = false;
            currentSpeed = tiredSpeed;
        }
        else if(isTired)
        {
            currentSpeed=tiredSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && !isTired) // Si está corriendo y tiene stamina
        {
            currentSpeed = sprintSpeed;
        }
        else // Si no está corriendo (presionando Shift) y tiene suficiente stamina, se mueve a la velocidad básica
        {
            currentSpeed = moveSpeed;
        }

        // Movimiento
        Vector2 targetPosition = rb.position + movement.normalized * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);

        // Rotación
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.MoveRotation(angle); // Usar MoveRotation en lugar de ajustar la rotación directamente
    }

    // Método para verificar si la stamina es baja
    public bool IsStaminaLow()
    {
        return stamina < staminaLowThreshold;
    }
}
