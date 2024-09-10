using BaseClass;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapSpawnList
{
    public string key; // Å° (string)
    public MapInfo value; // °ª (MonsterInfo)
}

[CreateAssetMenu(fileName = "SpawnMapData", menuName = "ScriptableObjects/SpawnMap", order = 1)]
public class SO_SpawnMap : ScriptableObject
{
    [HideInInspector] public Dictionary<string, MapInfo> mapSpawnDic;

    [SerializeField] private List<MapSpawnList> SpawnDicDisplay;

    public void UpdateInspectorDisplay()
    {
        SpawnDicDisplay = new List<MapSpawnList>();

        foreach (var kvp in mapSpawnDic)
        {
            MapSpawnList Test = new MapSpawnList();
            Test.key = kvp.Key;
            Test.value = kvp.Value;
            SpawnDicDisplay.Add(Test);
        }
    }
}