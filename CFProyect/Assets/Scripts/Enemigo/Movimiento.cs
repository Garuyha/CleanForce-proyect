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
    public void NuevoPunto(int i,int auxI, GameObject[] puntosPatrulla, Transform target)
    {
        do
        {
            i = Random.Range(0, puntosPatrulla.Length - 1);
        } while ((i == puntosPatrulla.Length) || (auxI == i));
        auxI = i;
        target = puntosPatrulla[i].transform;
    }
}