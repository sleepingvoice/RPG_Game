using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseClass;

public class SpawnMgr : MonoBehaviour
{
    #region ΩÃ±€≈Ê

    private static SpawnMgr instance = null;
    public static SpawnMgr Ins
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #endregion

    [SerializeField] private MonsterPool poolObj;

    [SerializeField] private SO_Monster monsterData;
    [SerializeField] private SO_SpawnMap spawnInfo;

    [HideInInspector] public MapInfo Info;


}
