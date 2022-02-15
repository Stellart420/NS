using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;

    [SerializeField] float speed;
    [SerializeField] int health = 3;

    [SerializeField] int maxHealth = 3;

    Camera cam;
    NavMeshAgent agent;
    Animator animator;
    public delegate void OnHealthChangeDelegate(float speed);
    public event OnHealthChangeDelegate OnHealthChange;

    public UnityAction CrystalTakeAction;

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            if (speed == value)
                return;

            speed = value;
            agent.speed = speed;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (health == value)
                return;

            health = value >= maxHealth ? maxHealth : value;
            OnHealthChange?.Invoke(health);
        }
    }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = speed;
    }

    private void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                transform.LookAt(agent.destination);
            }
        }
        animator.SetBool("IsRun", agent.remainingDistance > 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {
            Health++;
            CrystalTakeAction?.Invoke();
            Destroy(other.gameObject);
        }
    }
}
