using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class CrystalController : BaseController<CrystalController>
{

    public const string Tag = "Crystal";
    
    int initialSpawn;

    float spawnDelayMin;
    float spawnDelayMax;

    int maxCrytal;

    [SerializeField] SpawnMethod spawnMethod = SpawnMethod.Random;

    ObjectPool objectPooler;

    List<Crystal> crystals = new List<Crystal>();

    public Action<int> CrystalCount;


    public List<Transform> GetCrystalTransform()
    {
        var _transforms = new List<Transform>();
        crystals.ForEach(crystal => _transforms.Add(crystal.transform));
        return _transforms;
    }

    protected override void Initialization()
    {
        objectPooler = ObjectPool.Instance;
        maxCrytal = GameData.MaxCrystals;
        spawnDelayMin = GameData.MinDelaySpawnCrystal;
        spawnDelayMax = GameData.MaxDelaySpawnCrystal;
        initialSpawn = GameData.InitialCrystals;
        StartCoroutine(SpawnCrystal());
    }

    IEnumerator SpawnCrystal()
    {
        WaitForSeconds wait = new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));

        while (GameController.Instance.State == GameState.Loose)
            yield return wait;

        SpawnRandom(initialSpawn);

        while (true)
        {
            yield return wait;

            if (GameController.Instance.State == GameState.Loose)
                continue;

            if (spawnMethod == SpawnMethod.Random)
            {
                SpawnRandom();
            }
        }
    }

    void SpawnRandom(int _count = 1)
    {
        for (int i = 0; i < _count; i++)
        {
            if (crystals.Count >= maxCrytal)
                return;

            Vector3 spawnLocation = GameController.Instance.GetRandomGameBoardLocation();

            NavMeshHit hit;

            if (NavMesh.SamplePosition(spawnLocation, out hit, 2f, 1))
            {
                var crystal = objectPooler.SpawnFromPool(Tag, hit.position + Vector3.up / 2, Quaternion.identity).GetComponent<Crystal>();
                crystals.Add(crystal);
                CrystalCount?.Invoke(crystals.Count);
            }
        }
    }

    public void PickUp(Crystal _crystal)
    {
        crystals.Remove(_crystal);
        _crystal.PickUp();
    }

    public void Reset()
    {
        crystals.ForEach(crystal => crystal.gameObject.SetActive(false));
        crystals.Clear();
        SpawnRandom(initialSpawn);
        CrystalCount?.Invoke(crystals.Count);
    }
}

public enum SpawnMethod
{
    RoundPlayer,
    Random,
    InArea,
}
