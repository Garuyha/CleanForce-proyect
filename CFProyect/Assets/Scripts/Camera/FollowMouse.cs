using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Transform player; 
    public float maxDistance = 5f; 
    public float followSpeed = 5f; 

    void Update()
    {
        if (player == null) return;


        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);


        Vector2 direction = mousePosition - (Vector2)player.position;


        if (direction.magnitude > maxDistance)
        {
            direction = direction.normalized * maxDistance;
        }

        Vector2 targetPosition = (Vector2)player.position + direction;


        Vector2 smoothedPosition = Vector2.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z); 
    }
}
