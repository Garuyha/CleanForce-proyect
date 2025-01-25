using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Movimiento : MonoBehaviour
{
    public void Moverse(NavMeshAgent agent, Transform target)
    {
        agent.SetDestination(target.position);
    }
    public Transform NuevoPunto(GameObject[] puntosPatrulla, Transform target)
    {
        int i;
        do
        {
            i = Random.Range(0, puntosPatrulla.Length - 1);
        } while ((i == puntosPatrulla.Length) || (puntosPatrulla[i].transform == target));
        return target = puntosPatrulla[i].transform;
    }
}