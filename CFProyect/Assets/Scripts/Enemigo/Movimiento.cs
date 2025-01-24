using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExampleClass : MonoBehaviour
{
    //nuevoCuadrado = Instantiate(cuadradoNegro, transform.position, Quaternion.identity);
    [SerializeField] Transform target;
    [SerializeField] GameObject[] puntosPatrulla;
    [SerializeField] private int i = 0;
    [SerializeField] private float Wait=0;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false; 
        target = puntosPatrulla[i].transform;
    }

    private void Update()
    {
        agent.SetDestination(target.position);
        Wait = Wait+1*Time.deltaTime;
        if(Wait > 10)
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