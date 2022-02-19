using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GameController : BaseController<GameController>
{

    private GameState state = GameState.Loose;

    public GameState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            ChangeState?.Invoke(state);
        }
    }

    int score;

    public int Score => score;
    int scoreAdded;

    float closestToEnemy;
    public float ClosestToEnemy
    {
        get
        {
            return closestToEnemy;
        }
        set
        {
            closestToEnemy = value;
            ClosestEnemy?.Invoke(closestToEnemy);
        }
    }

    float closestToCrystal;

    public float ClosestToCrystal
    {
        get
        {
            return closestToCrystal;
        }
        set
        {
            closestToCrystal = value;
            ClosestCrystal?.Invoke(closestToCrystal);
        }
    }

    public Action<int> ChangeScore;
    public Action<GameState> ChangeState;
    public Action<float> ClosestEnemy;
    public Action<float> ClosestCrystal;

    CrystalController crystalController;
    EnemyController enemyController;
    PlayerController player;
    protected override void Initialization()
    {
        scoreAdded = GameData.CrystalAddedScore;
        player = PlayerController.Instance;
        player.OnCollided += CheckCollidedPlayer;
        crystalController = CrystalController.Instance;
        enemyController = EnemyController.Instance;
        player.OnHealthChange += (health, immune) =>
        {
            if (health <= 0)
                Loose();
        };
    }

    void CheckCollidedPlayer(Collider col)
    {
        if (col.tag == CrystalController.Tag)
        {
            var crystal = col.GetComponent<Crystal>();
            crystalController.PickUp(crystal);
            AddScore(scoreAdded);
        }
    }

    public void Play()
    {
        State = GameState.Play;
    }
        
    void AddScore(int _count)
    {
        score += _count;
        ChangeScore?.Invoke(score);
    }

    private void Update()
    {
        if (state == GameState.Loose)
            return;

        ClosestToEnemy = ChooseClosest(enemyController.GetEnemiesTransform(), player.transform);
        ClosestToCrystal = ChooseClosest(crystalController.GetCrystalTransform(), player.transform);
    }

    public void Reset()
    {
        enemyController.Reset();
        crystalController.Reset();
        State = GameState.Play;
        score = 0;
        ChangeScore?.Invoke(score);
        player.Reset();
    }

    public void Loose()
    {
        State = GameState.Loose;
    }

    private float ChooseClosest(List<Transform> _objects, Transform _from)
    {
        float closestDistance = float.MaxValue;

        NavMeshPath path;

        if (_objects == null || _objects.Count == 0)
            return 0;

        _objects.ForEach(obj =>
        {

            path = new NavMeshPath();
            if (NavMesh.CalculatePath(_from.position, obj.position, 1, path))
            {
                float distance = Vector3.Distance(_from.position, path.corners[0]);

                for (int i = 1; i < path.corners.Length; i++)
                {
                    distance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
                }

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                }
            }
        });

        return closestDistance;
    }

    //ѕолучение рандомной точки на NavMesh
    public Vector3 GetRandomGameBoardLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int maxIndices = navMeshData.indices.Length - 3;

        int firstVertexSelected = Random.Range(0, maxIndices);
        int secondVertexSelected = Random.Range(0, maxIndices);

        Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];

        Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
        Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];


        if ((int)firstVertexPosition.x == (int)secondVertexPosition.x || (int)firstVertexPosition.z == (int)secondVertexPosition.z)
        {
            point = GetRandomGameBoardLocation(); //мен€ не устраивает эта рекурси€, могло быть и лучше 
        }
        else
        {
            point = Vector3.Lerp(firstVertexPosition, secondVertexPosition, Random.Range(0.05f, 0.95f));
        }

        return point;
    }   
}
public enum GameState
{
    Play,
    Loose,
}
