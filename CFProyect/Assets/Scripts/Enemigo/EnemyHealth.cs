using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyHealth : MonoBehaviour
{

    public int TakeDamage(int damage, int currentHealth)
    {
        currentHealth -= damage;
        return currentHealth;
    }

    public Transform DieMovment(NavMeshAgent agent, Transform target)
    {
        return target = agent.transform;
    }

    public SpriteRenderer DieSprite(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.color = Color.green;
        return spriteRenderer;
    }
}
