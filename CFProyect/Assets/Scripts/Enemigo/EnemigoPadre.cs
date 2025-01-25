using UnityEngine;
using UnityEngine.AI;

public class EnemigoPadre : MonoBehaviour
{
    [SerializeField] public GameObject mugre;
    [SerializeField] public Transform enemigo;
    [SerializeField] private SpawnMugre spawn;
    [SerializeField] private Movimiento movimiento;
    [SerializeField] public EnemyHealth sistemaDmg;
    [SerializeField] private float wait = 0;
    [SerializeField] public Transform target;
    [SerializeField] public GameObject[] puntosPatrulla;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int currentHealth;
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = puntosPatrulla[0].transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentHealth = maxHealth;
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
            target = movimiento.NuevoPunto(puntosPatrulla, target);
        }
    }

    public void DealDamage (int damage)
    {
        currentHealth = sistemaDmg.TakeDamage(damage,currentHealth);
        if (currentHealth <= 0)
        {
            Muerte();
        }
    }
    private void Muerte()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        target = sistemaDmg.DieMovment(agent, target);
        spriteRenderer = sistemaDmg.DieSprite(spriteRenderer);

    }

}
