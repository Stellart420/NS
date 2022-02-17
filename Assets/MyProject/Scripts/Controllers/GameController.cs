using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    //todo � ���� ����-���������
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

    //��������� ��������� ����� �� NavMesh
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
            point = GetRandomGameBoardLocation(); //���� �� ���������� ��� ��������, ����� ���� � ����� 
        }
        else
        {
            point = Vector3.Lerp(firstVertexPosition, secondVertexPosition, Random.Range(0.05f, 0.95f));
        }

        return point;
    }
}
