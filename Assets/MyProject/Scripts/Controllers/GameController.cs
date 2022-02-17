using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    //todo В ОКНО ГЕЙМ-ДИЗАЙНЕРА
    public int scoreAdded = 1;

    int score;

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
        CrystalController.Instance.PickUpAction += () =>
        {
            AddScore(1);
        };
    }

    void AddScore(int _count)
    {
        score += _count;
    }

    //Получение рандомной точки на NavMesh
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
            point = GetRandomGameBoardLocation(); //меня не устраивает эта рекурсия, могло быть и лучше 
        }
        else
        {
            point = Vector3.Lerp(firstVertexPosition, secondVertexPosition, Random.Range(0.05f, 0.95f));
        }

        return point;
    }
}
