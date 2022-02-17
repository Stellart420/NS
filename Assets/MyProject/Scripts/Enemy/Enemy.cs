using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IPooledObject
{
    [SerializeField] float speed = 1.5f;

    NavMeshAgent agent;

    public void OnObjectSpawn()
    {
        agent.Warp(transform.position);
        agent.enabled = true;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
    
    void GotoNextPoint()
    {
        agent.SetDestination(GameController.Instance.GetRandomGameBoardLocation());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == CrystalController.Tag)
        {
            CrystalController.Instance.PickUp(true);
            other.gameObject.SetActive(false);
        }
    }

}
