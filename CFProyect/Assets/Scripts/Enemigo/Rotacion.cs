using UnityEngine;
using UnityEngine.AI;

public class Rotacion : MonoBehaviour
{
    //public GameObject target;
    public float speed;
    public float rotationModifier;

    public void Rotar(NavMeshAgent target)
    {
        if (target != null)
        {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        }
    }
}