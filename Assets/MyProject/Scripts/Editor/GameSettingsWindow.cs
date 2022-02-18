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

    [MenuItem("Игра/Настройки")]
    public static void ShowWindow()
    {
        GetWindow<GameSettingsWindow>(false, "Настройки", true);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Сохранить"))
        {
            GameData.SaveData();
        }

        tab = GUILayout.Toolbar(tab, new string[] { "Игрок", "Кристаллы", "Враг"});
        switch (tab)
        {
            case 2:
                #region Enemy

                enemySpeed = GameData.EnemySpeed;
                enemySpawnDelay = GameData.EnemySpawnDelay;
                enemyMax = GameData.EnemyMax;

                GUILayout.Label("Враг", EditorStyles.boldLabel);
                enemySpeed = EditorGUILayout.FloatField("Скорость", enemySpeed);
                enemySpawnDelay = EditorGUILayout.FloatField("Время спавна", enemySpawnDelay);
                enemyMax = EditorGUILayout.IntField("Макс количество", enemyMax);

                GameData.EnemySpeed = enemySpeed;
                GameData.EnemySpawnDelay = enemySpawnDelay;
                GameData.EnemyMax = enemyMax;

                #endregion
                return;
            case 1:
                #region Кристаллы
                maxCrystals = GameData.MaxCrystals;
                initialCrystals = GameData.InitialCrystals;
                spawnDelayMax = GameData.MaxDelaySpawnCrystal;
                spawnDelayMin = GameData.MinDelaySpawnCrystal;
                addedScore = GameData.CrystalAddedScore;

                GUILayout.Label("Кристаллы", EditorStyles.boldLabel);
                initialCrystals = EditorGUILayout.IntField("Стартовое количество", initialCrystals);
                maxCrystals = EditorGUILayout.IntField("Максимум кристаллов", maxCrystals);
                spawnDelayMin = EditorGUILayout.FloatField("Мин время спавна", spawnDelayMin);
                spawnDelayMax = EditorGUILayout.FloatField("Макс время спавна", spawnDelayMax);
                addedScore = EditorGUILayout.IntField("Добавляет очков", addedScore);
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

                GUILayout.Label("Игрок", EditorStyles.boldLabel);
                playerSpeed = EditorGUILayout.FloatField("Скорость", playerSpeed);
                playerMaxHealth = EditorGUILayout.IntField("Максимально жизней", playerMaxHealth);
                immuneTime = EditorGUILayout.FloatField("Время иммунитета", immuneTime);

                GameData.ImmuneTime = immuneTime;
                GameData.PlayerSpeed = playerSpeed;
                GameData.PlayerMaxHealth = playerMaxHealth;

                #endregion
                break;
        }

        

        
    }
}
