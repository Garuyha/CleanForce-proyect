using UnityEngine;
using UnityEngine.AI;

public class EnemigoPadre : MonoBehaviour
{
    [SerializeField] public GameObject[] puntosPatrulla;
    [SerializeField] public GameObject mugre;
    [SerializeField] public Transform enemigo;
    [SerializeField] public Animator animaciones;
    [SerializeField] private SpawnMugre spawn;
    [SerializeField] private Movimiento movimiento;
    [SerializeField] private Rotacion rotar;
    [SerializeField] public EnemyHealth sistemaDmg;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float wait = 0;
    [SerializeField] public Transform target;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int currentHealth;
    public NavMeshAgent agent;
    public NavMeshAgent sombra;
    public bool vivo = true;
    [SerializeField] private int maximaMugre;
    public static int mugreActual;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        target = puntosPatrulla[0].transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentHealth = maxHealth;
        mugreActual = 0;
    }

    private void Update()
    {
        movimiento.Moverse(agent, target);
        if (vivo)
        {
            movimiento.Moverse(sombra, target);
            rotar.Rotar(sombra);
            wait = wait + 1 * Time.deltaTime;
            if (wait >= 0.5f && mugreActual < maximaMugre)
            {
                animaciones.SetBool("Da�o", false);
                wait = 0f;
                mugreActual++;
                spawn.Crear(mugre, enemigo, vivo);
            }
        }else
            target = sistemaDmg.DieMovment(agent, target);
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
        animaciones.SetBool("Da�o", true);
        animaciones.SetInteger("vida", currentHealth);
        if (currentHealth <= 0)
        {
            Muerte();
        }
    }
    private void Muerte()
    {
        target = sistemaDmg.DieMovment(agent, target);
        animaciones.SetBool("Da�o", false);
        vivo = false; 
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    
}
