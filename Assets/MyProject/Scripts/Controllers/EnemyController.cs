using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

    public const string Tag = "Enemy";

    //todo œ≈–≈Õ≈—“» ¬ Œ ÕŒ √≈…Ã-ƒ»«¿…Õ≈–¿
    [SerializeField] float spawnDelay;
    [SerializeField] int maxEnemies;
    [SerializeField] SpawnMethod spawnMethod = SpawnMethod.RoundPlayer;

    List<Enemy> enemies = new List<Enemy>();

    [SerializeField] List<Collider> spawnAreas = new List<Collider>();

    ObjectPool objectPooler;
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
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        while (true)
        {
            yield return wait;
            if (spawnMethod == SpawnMethod.RoundPlayer)
            {
                SpawnInArea();
            }
        }
    }

    void SpawnInArea(int _count = 1)
    {
        for (int i = 0; i < _count; i++)
        {
            if (enemies.Count >= maxEnemies)
                return;


            var spawn_area = spawnAreas[Random.Range(0, spawnAreas.Count)];

            var size = spawn_area.bounds.size;

            var spawn_pos = spawn_area.bounds.center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));

            NavMeshHit hit;

            if (NavMesh.SamplePosition(spawn_pos, out hit, 2f, 1))
            {
                var enemy = objectPooler.SpawnFromPool(Tag, hit.position + Vector3.up / 2, Quaternion.identity).GetComponent<Enemy>();

                enemies.Add(enemy);
            }
        }
    }
}
