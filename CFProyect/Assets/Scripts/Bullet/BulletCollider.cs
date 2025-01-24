using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        Destroy(gameObject);
    }
}
