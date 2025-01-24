using UnityEngine;
using UnityEngine.AI;

public class ExampleClass : MonoBehaviour
{
    //nuevoCuadrado = Instantiate(cuadradoNegro, transform.position, Quaternion.identity);
    [SerializeField] Transform target;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        agent.SetDestination(target.position);
    }
}