using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;

    private Vector2 movement;
    private Vector2 mousePos;

    void Update()
    {
        // Leer entrada del teclado
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Leer la posición del mouse en coordenadas del mundo
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // Movimiento
        Vector2 targetPosition = rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);

        // Rotación
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.MoveRotation(angle); // Usar MoveRotation en lugar de ajustar la rotación directamente
    }
}
