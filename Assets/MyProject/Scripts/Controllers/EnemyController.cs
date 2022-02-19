using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : BaseController<EnemyController>
{

    public const string Tag = "Enemy";

    float spawnDelay;
    int maxEnemies;
    [SerializeField] SpawnMethod spawnMethod = SpawnMethod.RoundPlayer;

    List<Enemy> enemies = new List<Enemy>();

    [SerializeField] List<Collider> spawnAreas = new List<Collider>();

    ObjectPool objectPooler;

    public Action<int> EnemiesCount;
    public List<Transform> GetEnemiesTransform()
    {
        var _transforms = new List<Transform>();
        enemies.ForEach(enemy => _transforms.Add(enemy.transform));
        return _transforms;
    }

    protected override void Initialization()
    {
        objectPooler = ObjectPool.Instance;
        spawnDelay = GameData.EnemySpawnDelay;
        maxEnemies = GameData.EnemyMax;
        GameController.Instance.ChangeState += ChangeGameState;
        StartCoroutine(SpawnEnemy());
    }

    void ChangeGameState(GameState state)
    {
        bool can_move = state == GameState.Play;
        enemies.ForEach(enemy => enemy.CanMove(can_move));
    }

    public void Reset()
    {
        enemies.ForEach(enemy => enemy.gameObject.SetActive(false));
        enemies.Clear();
        EnemiesCount?.Invoke(0);
    }

    IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        while (true)
        {
            yield return wait;
            
            if (GameController.Instance.State == GameState.Loose)
                continue;
            
            if (spawnMethod == SpawnMethod.InArea)
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
                
                enemy.collided += (collider) => 
                {
                    var crystal = collider.GetComponent<Crystal>();
                    if (crystal != null)
                        CrystalController.Instance.PickUp(crystal);
                };
                enemies.Add(enemy);
                EnemiesCount?.Invoke(enemies.Count);
            }
        }
    }
}
