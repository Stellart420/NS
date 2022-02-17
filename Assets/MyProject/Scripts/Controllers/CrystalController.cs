using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CrystalController : MonoBehaviour
{
    public static CrystalController Instance;

    public const string Tag = "Crystal";
    [SerializeField] Transform container;
    [SerializeField] int startSpawnCount = 5;

    //todo Переделать для гейм дизайнера
    [SerializeField] float spawnDelayMin;
    [SerializeField] float spawnDelayMax;

    [SerializeField] int maxCrytal;

    [SerializeField] SpawnMethod spawnMethod = SpawnMethod.Random;

    int counter = 0;

    ObjectPool objectPooler;

    public UnityAction PickUpAction;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;


    }

    private void Start()
    {
        objectPooler = ObjectPool.Instance;
        SpawnRandom(startSpawnCount);
        StartCoroutine(SpawnCrystal());

    }

    IEnumerator SpawnCrystal()
    {
        WaitForSeconds wait = new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));

        while (true)
        {
            yield return wait;
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
            if (counter >= maxCrytal)
                return;

            Vector3 spawnLocation = GameController.Instance.GetRandomGameBoardLocation();

            NavMeshHit hit;

            if (NavMesh.SamplePosition(spawnLocation, out hit, 2f, 1))
            {
                objectPooler.SpawnFromPool(Tag, hit.position + Vector3.up / 2, Quaternion.identity);

                counter++;
            }
        }
    }

    public void PickUp(bool is_enemy = false)
    {
        counter--;

        if (!is_enemy)
        {
            PickUpAction?.Invoke();
        }
    }
}

public enum SpawnMethod
{
    RoundPlayer,
    Random,
    InArea,
}
