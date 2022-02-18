using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseController<EnemyController>
{

    public const string Tag = "Enemy";

    float spawnDelay;
    int maxEnemies;
    [SerializeField] SpawnMethod spawnMethod = SpawnMethod.RoundPlayer;

    List<Enemy> enemies = new List<Enemy>();

    [SerializeField] List<Collider> spawnAreas = new List<Collider>();

    Enemy closestEnemy;

    PlayerController player;
    ObjectPool objectPooler;

    protected override void Initialization()
    {
        player = PlayerController.Instance;
        objectPooler = ObjectPool.Instance;
        spawnDelay = GameData.EnemySpawnDelay;
        maxEnemies = GameData.EnemyMax;
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        ChooseClosest();
    }

    private float ChooseClosest()
    {
        float closestDistance = float.MaxValue;

        NavMeshPath path;

        enemies.ForEach(enemy =>
        {

            path = new NavMeshPath();
            if (NavMesh.CalculatePath(player.transform.position, enemy.transform.position, 1, path))
            {
                float distance = Vector3.Distance(player.transform.position, path.corners[0]);

                for (int i = 1; i < path.corners.Length; i++)
                {
                    distance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
                }

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        });

        return closestDistance;
    }

    IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        while (true)
        {
            yield return wait;
            
            if (GameController.Instance.State == GameState.Pause)
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
            }
        }
    }
}
