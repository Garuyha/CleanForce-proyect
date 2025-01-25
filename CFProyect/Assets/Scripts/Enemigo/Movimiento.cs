using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Movimiento : MonoBehaviour
{
    //nuevoCuadrado = Instantiate(cuadradoNegro, transform.position, Quaternion.identity);
    [SerializeField] public Transform target;
    [SerializeField] private GameObject[] puntosPatrulla;
    [SerializeField] private int i = 0;
    [SerializeField] private float Wait = 0;
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = puntosPatrulla[i].transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        agent.SetDestination(target.position);
        Wait = Wait + 1 * Time.deltaTime;
        if (Wait > 10)
        {
            Wait = 0;
            do
            {
                i = Random.Range(0, puntosPatrulla.Length - 1);
                Debug.Log("punto destino: " + i);
            } while (i == puntosPatrulla.Length);
            target = puntosPatrulla[i].transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Negro")
        {
            Wait = 0;
            do
            {
                i = Random.Range(0, puntosPatrulla.Length - 1);
                Debug.Log("punto destino: " + i);
            } while (i == puntosPatrulla.Length);
            target = puntosPatrulla[i].transform;
        }
    }
}