using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : BaseController<ObjectPool>
{
    [Serializable]
    public class Pool
    {
        public bool shouldExpand = true;
        public string tag;
        public GameObject prefab;
        public int size;
        public Transform container;
    }

    protected override void Initialization()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, pool.container);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        return SpawnFromPool(tag, position, Quaternion.identity, null);
    }

    public GameObject SpawnFromPool(string tag, Transform parent)
    {
        return SpawnFromPool(tag, Vector3.zero, Quaternion.identity, parent);
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        return SpawnFromPool(tag, position, rotation, null);
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }


        GameObject objectToSpawn = poolDictionary[tag].FirstOrDefault(go => !go.activeInHierarchy);
        if (objectToSpawn != null)
        {
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            if (parent != null)
                objectToSpawn.transform.parent = parent;

            objectToSpawn.transform.localScale = pools.Find(pool => pool.tag == tag).prefab.transform.localScale;
            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }

            return objectToSpawn;
        }

        foreach (Pool pool in pools)
        {
            if (pool.tag == tag)
            {
                if (pool.shouldExpand)
                {
                    objectToSpawn = Instantiate(pool.prefab, position, rotation, pool.container);
                    objectToSpawn.SetActive(true);
                    IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

                    if (pooledObj != null)
                    {
                        pooledObj.OnObjectSpawn();
                    }
                    poolDictionary[tag].Add(objectToSpawn);
                    return objectToSpawn;
                }
            }
        }

        return null;

    }
}
