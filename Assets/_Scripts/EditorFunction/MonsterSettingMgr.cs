using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class MonsterSettingMgr : MonoBehaviour
{
    public SO_ModelAddress ModelAddress;
    public GameObject SetModelPrefab;

    public string TargetModel_ID;

    private List<GameObject> MonsterList = new List<GameObject>();

    public void CreatePrefabModel(Vector3 TargetPos)
    {
        var prefabs = Instantiate(SetModelPrefab, this.transform);
        prefabs.transform.position = TargetPos;
        prefabs.name = TargetModel_ID;
        MonsterList.Add(prefabs);
        if (ModelAddress)
        {
            GameObject ModelPrefab = ModelAddress.CheckList(TargetModel_ID);
            if (ModelPrefab != null)
            {
                Instantiate(ModelPrefab, prefabs.transform);

            }
        }
        Debug.Log(MonsterList.Count);
    }

    public void ListCheck()
    {
        MonsterList = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).GetComponent<GameObject>();
            MonsterList.Add(child);
        }
        Debug.Log(MonsterList.Count);
    }

    public void ClearList()
    {
        MonsterList = new List<GameObject>();

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
    }
}
