using System;
using System.Collections.Generic;
using UnityEngine;
using BaseClass;
using System.Text;

[Serializable]
public class MapList
{
    public string key; // Å° (string)
    public MonsterInfo value; // °ª (MonsterInfo)
}

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/Monster", order = 1)]
public class SO_Monster : ScriptableObject
{
    [HideInInspector] public Dictionary<string, MonsterInfo> monsterDic;

    [SerializeField] private List<MapList> monsterDicDisplay;

    public void UpdateInspectorDisplay()
    {
        monsterDicDisplay = new List<MapList>();
        
        foreach (var kvp in monsterDic)
        {
            MapList Test = new MapList();
            Test.key = kvp.Key;
            Test.value = kvp.Value;
            monsterDicDisplay.Add(Test);
        }
    }
}
