using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GameController : BaseController<GameController>
{

    private GameState state = GameState.Pause;

    public GameState State => state;

    int score;

    int scoreAdded;

    public Action<int> ChangeScore;
    protected override void Initialization()
    {
        scoreAdded = GameData.CrystalAddedScore;
        PlayerController.Instance.OnCollided += CheckCollidedPlayer;
    }

    void CheckCollidedPlayer(Collider col)
    {
        if (col.tag == CrystalController.Tag)
        {
            var crystal = col.GetComponent<Crystal>();
            CrystalController.Instance.PickUp(crystal);
            AddScore(scoreAdded);
        }
    }

    public void Play()
    {
        state = GameState.Play;
    }
        
    void AddScore(int _count)
    {
        score += _count;
        ChangeScore?.Invoke(score);
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
    Pause,
}
