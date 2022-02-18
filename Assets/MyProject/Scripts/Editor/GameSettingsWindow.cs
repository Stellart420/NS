using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameSettingsWindow : EditorWindow
{
    [SerializeField] int tab = 0;

    #region Player_Var
    [SerializeField] int playerMaxHealth = 3;
    [SerializeField] float playerSpeed = 3f;
    [SerializeField] float immuneTime = 3f;
    #endregion
    #region Crystal_Var
    [SerializeField] int initialCrystals = 5;
    [SerializeField] int maxCrystals = 10;
    [SerializeField] float spawnDelayMin, spawnDelayMax = 5f;
    [SerializeField] int addedScore = 1;
    #endregion
    #region Enemy_Var
    [SerializeField] float enemySpeed = 5;
    [SerializeField] float enemySpawnDelay = 10;
    [SerializeField] int enemyMax = 5;
    #endregion

    [MenuItem("����/���������")]
    public static void ShowWindow()
    {
        GetWindow<GameSettingsWindow>(false, "���������", true);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("���������"))
        {
            GameData.SaveData();
        }

        tab = GUILayout.Toolbar(tab, new string[] { "�����", "���������", "����"});
        switch (tab)
        {
            case 2:
                #region Enemy

                enemySpeed = GameData.EnemySpeed;
                enemySpawnDelay = GameData.EnemySpawnDelay;
                enemyMax = GameData.EnemyMax;

                GUILayout.Label("����", EditorStyles.boldLabel);
                enemySpeed = EditorGUILayout.FloatField("��������", enemySpeed);
                enemySpawnDelay = EditorGUILayout.FloatField("����� ������", enemySpawnDelay);
                enemyMax = EditorGUILayout.IntField("���� ����������", enemyMax);

                GameData.EnemySpeed = enemySpeed;
                GameData.EnemySpawnDelay = enemySpawnDelay;
                GameData.EnemyMax = enemyMax;

                #endregion
                return;
            case 1:
                #region ���������
                maxCrystals = GameData.MaxCrystals;
                initialCrystals = GameData.InitialCrystals;
                spawnDelayMax = GameData.MaxDelaySpawnCrystal;
                spawnDelayMin = GameData.MinDelaySpawnCrystal;
                addedScore = GameData.CrystalAddedScore;

                GUILayout.Label("���������", EditorStyles.boldLabel);
                initialCrystals = EditorGUILayout.IntField("��������� ����������", initialCrystals);
                maxCrystals = EditorGUILayout.IntField("�������� ����������", maxCrystals);
                spawnDelayMin = EditorGUILayout.FloatField("��� ����� ������", spawnDelayMin);
                spawnDelayMax = EditorGUILayout.FloatField("���� ����� ������", spawnDelayMax);
                addedScore = EditorGUILayout.IntField("��������� �����", addedScore);
                EditorGUILayout.EndFoldoutHeaderGroup();

                GameData.MaxCrystals = maxCrystals;
                GameData.InitialCrystals = initialCrystals;
                GameData.MaxDelaySpawnCrystal = spawnDelayMax;
                GameData.MinDelaySpawnCrystal = spawnDelayMin;
                GameData.CrystalAddedScore = addedScore;
                #endregion
                break;
            default:
                #region Player

                playerSpeed = GameData.PlayerSpeed;
                playerMaxHealth = GameData.PlayerMaxHealth;
                immuneTime = GameData.ImmuneTime;

                GUILayout.Label("�����", EditorStyles.boldLabel);
                playerSpeed = EditorGUILayout.FloatField("��������", playerSpeed);
                playerMaxHealth = EditorGUILayout.IntField("����������� ������", playerMaxHealth);
                immuneTime = EditorGUILayout.FloatField("����� ����������", immuneTime);

                GameData.ImmuneTime = immuneTime;
                GameData.PlayerSpeed = playerSpeed;
                GameData.PlayerMaxHealth = playerMaxHealth;

                #endregion
                break;
        }

        

        
    }
}
