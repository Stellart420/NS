using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;

    [SerializeField] float speed;

    [SerializeField] int health = 3;

    [SerializeField] int maxHealth = 3;
    [SerializeField] float immuneTime = 3f;

    Camera cam;
    NavMeshAgent agent;
    Animator animator;
    public delegate void OnHealthChangeDelegate(float speed);
    public event OnHealthChangeDelegate OnHealthChange;

    public UnityAction CrystalTakeAction;

    private const string IsRun = "IsRun";
    private RaycastHit[] hit = new RaycastHit[1];

    bool immune = false;

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
            

            if (Physics.RaycastNonAlloc(ray, hit) > 0)
            {
                agent.SetDestination(hit[0].point);
                //transform.LookAt(agent.destination); //Вариант для быстрого поворота в сторону точки движения
            }
        }
        //agent.velocity.magnitude > 0.02f
        animator.SetBool(IsRun, Mathf.Abs(Vector3.Distance(agent.destination,transform.position)) > 0.2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == EnemyController.Tag)
        {
            print("Col with enemy");
            if (!immune)
            {
                health--;
                StartCoroutine(Immune());
            }
        }
    }

    IEnumerator Immune()
    {
        immune = true;
        yield return new WaitForSeconds(immuneTime);
        immune = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {
            Health++;
            CrystalController.Instance.PickUp();
            other.gameObject.SetActive(false);
        }
    }
}
