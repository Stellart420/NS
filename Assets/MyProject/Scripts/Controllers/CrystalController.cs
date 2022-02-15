using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    public static CrystalController Instance;

    [SerializeField] GameObject crystalPrefab;
    [SerializeField] float maxCrytal;

    int currentCrystalCount;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


}
