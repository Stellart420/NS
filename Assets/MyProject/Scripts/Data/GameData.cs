using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameData : ScriptableObject
{
    #region Player
    public static float PlayerSpeed
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetFloat("PlayerSpeed", 3f);
#else
            return PlayerPrefs.GetFloat("PlayerSpeed", 3f);
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetFloat("PlayerSpeed", value);
#else
            PlayerPrefs.SetFloat("PlayerSpeed", value);
#endif
        }
    }
    public static int PlayerMaxHealth
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetInt("PlayerMaxHealth", 3);
#else
            return PlayerPrefs.GetInt("PlayerMaxHealth", 3);
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetInt("PlayerMaxHealth", value);
#else
            PlayerPrefs.SetInt("PlayerMaxHealth", value);
#endif
        }
    }
    public static float ImmuneTime
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetFloat("ImmuneTime", 3f);
#else
            return PlayerPrefs.GetFloat("ImmuneTime", 3f);
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetFloat("ImmuneTime", value);
#else
            PlayerPrefs.SetFloat("ImmuneTime", value);
#endif
        }
    }
    #endregion

    #region Crystal
    public static int InitialCrystals
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetInt("InitialCrystals", 3);
#else
            return PlayerPrefs.GetInt("InitialCrystals", 3);
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetInt("InitialCrystals", value);
#else
            PlayerPrefs.SetInt("InitialCrystals", value);
#endif
        }
    }
    public static int MaxCrystals
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetInt("MaxCrystals", 3);
#else
            return PlayerPrefs.GetInt("MaxCrystals", 3);
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetInt("MaxCrystals", value);
#else
            PlayerPrefs.SetInt("MaxCrystals", value);
#endif
        }
    }

    public static float MinDelaySpawnCrystal
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetFloat("MinDelaySpawnCrystal", 3f);
#else
            return PlayerPrefs.GetFloat("MinDelaySpawnCrystal", 3f);
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetFloat("MinDelaySpawnCrystal", value);
#else
            PlayerPrefs.SetFloat("MinDelaySpawnCrystal", value);
#endif
        }
    }

    public static float MaxDelaySpawnCrystal
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetFloat("MaxDelaySpawnCrystal", 3f);
#else
            return PlayerPrefs.GetFloat("MaxDelaySpawnCrystal", 3f);
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetFloat("MaxDelaySpawnCrystal", value);
#else
            PlayerPrefs.SetFloat("MaxDelaySpawnCrystal", value);
#endif
        }
    }

    public static int CrystalAddedScore
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetInt("CrystalAddedScore", 1);
#else
            return PlayerPrefs.GetInt("CrystalAddedScore", 1);
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetInt("CrystalAddedScore", value);
#else
            PlayerPrefs.SetInt("CrystalAddedScore", value);
#endif
        }
    }

    #endregion

    #region Enemy
    public static float EnemySpeed
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetFloat("EnemySpeed", 3f);
#else
            return PlayerPrefs.GetFloat("EnemySpeed", 3f);
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetFloat("EnemySpeed", value);
#else
            PlayerPrefs.SetFloat("EnemySpeed", value);
#endif
        }
    }
    public static float EnemySpawnDelay
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetFloat("EnemySpawnDelay", 3f);
#else
            return PlayerPrefs.GetFloat("EnemySpawnDelay", 3f);
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetFloat("EnemySpawnDelay", value);
#else
            PlayerPrefs.SetFloat("EnemySpawnDelay", value);
#endif
        }
    }
    public static int EnemyMax
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetInt("EnemyMax", 5);
#else
            return PlayerPrefs.GetInt("EnemyMax", 5);
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetInt("EnemyMax", value);
#else
            PlayerPrefs.SetInt("EnemyMax", value);
#endif
        }
    }
    #endregion
    public static void SaveData()
    {
        PlayerPrefs.SetFloat("PlayerSpeed", PlayerSpeed);
        PlayerPrefs.SetInt("PlayerMaxHealth", PlayerMaxHealth);
        PlayerPrefs.SetFloat("ImmuneTime", ImmuneTime);

        PlayerPrefs.SetInt("InitialCrystals", InitialCrystals);
        PlayerPrefs.SetInt("MaxCrystals", MaxCrystals);
        PlayerPrefs.SetFloat("MinDelaySpawnCrystal", MinDelaySpawnCrystal);
        PlayerPrefs.SetFloat("MaxDelaySpawnCrystal", MaxDelaySpawnCrystal);
        PlayerPrefs.SetInt("CrystalAddedScore", CrystalAddedScore);

        PlayerPrefs.SetFloat("EnemySpawnDelay", EnemySpawnDelay);
        PlayerPrefs.SetFloat("EnemySpeed", EnemySpeed);
        PlayerPrefs.SetInt("EnemyMax", EnemyMax);
    }
}
