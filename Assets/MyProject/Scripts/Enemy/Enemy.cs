using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IPooledObject
{
    [SerializeField] float speed = 1.5f;

    public Action<Collider> collided;

    NavMeshAgent agent;

    public void OnObjectSpawn()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.Warp(transform.position);
        agent.enabled = true;
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
            collided?.Invoke(other);
        }
    }

}
