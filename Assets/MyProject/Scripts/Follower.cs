using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    Transform target;
    Vector3 startingDistance;

    void Start()
    {
        target = PlayerController.Instance.transform;
        startingDistance = transform.position - target.position;
    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        transform.position = new Vector3(target.position.x + startingDistance.x, startingDistance.y, target.position.z + startingDistance.z);
    }
}
