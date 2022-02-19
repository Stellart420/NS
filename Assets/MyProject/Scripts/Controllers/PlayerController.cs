using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerController : BaseController<PlayerController>
{
    public const string Tag = "Player";

    int health;

    public Action<int, bool> OnHealthChange;
    public Action<Collider> OnCollided;

    float immuneTime;

    Camera cam;
    NavMeshAgent agent;
    Animator animator;
    

    private const string IsRun = "IsRun";
    private RaycastHit[] hit = new RaycastHit[1];

    bool immune = false;
    int maxHealth;


    Vector3 startPosition;

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


            if (value == 0)
            {
                Died();
                OnHealthChange?.Invoke(value, false);
                return;
            }

            bool immune = health > value;
            health = value >= maxHealth ? maxHealth : value;
            OnHealthChange?.Invoke(health, immune);
        }
    }

    protected override void Initialization()
    {
        startPosition = transform.position;

        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        maxHealth = GameData.PlayerMaxHealth;
        immuneTime = GameData.ImmuneTime;
        Health = maxHealth;
        agent.speed = GameData.PlayerSpeed;
    }

    private void Update()
    {
        if (GameController.Instance.State == GameState.Loose)
            return;

        CheckInput();
    }

    void Died()
    {
        //todo Анимация смерти
        agent.SetDestination(transform.position);
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

    public void Reset()
    {
        transform.position = startPosition;
        Health = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == EnemyController.Tag)
        {
            if (!immune)
            {
                Health--;
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
        OnCollided?.Invoke(other);
        if (other.tag == "Crystal")
        {
            Health++;
        }
    }
}
