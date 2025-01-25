using UnityEngine;
using UnityEngine.AI;

public class EnemigoPadre : MonoBehaviour
{
    [SerializeField] public GameObject mugre;
    [SerializeField] public Transform enemigo;
    [SerializeField] private SpawnMugre spawn;
    [SerializeField] private Movimiento movimiento;
    [SerializeField] private float wait = 0;
    [SerializeField] public Transform target;
    [SerializeField] public GameObject[] puntosPatrulla;
    [SerializeField] public int i = 0, auxI = 1;
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
        movimiento.Moverse(agent, target);
        wait = wait + 1 * Time.deltaTime;
        if (wait >= 0.5f)
        {
            wait = 0f;
            spawn.Crear(mugre, enemigo);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Negro")
        {
            movimiento.NuevoPunto(i, auxI, puntosPatrulla, target);
        }
    }

}
