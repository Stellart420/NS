using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crystal : MonoBehaviour, IPooledObject
{
    public void OnObjectSpawn()
    {
        
    }

    public void PickUp()
    {
        gameObject.SetActive(false);
    }

}
