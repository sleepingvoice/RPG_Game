using BaseClass;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelAddress", menuName = "ScriptableObjects/ModelAdress", order = 1)]
public class SO_ModelAddress : ScriptableObject
{
    [SerializeField] private List<string> ModelNameList;
    [SerializeField] private List<GameObject> ModelList;

    public void LoadMonsterModelPath()
    {
        PathListClass LoadPath = JsonUtility.FromJson<PathListClass>(File.ReadAllText(Application.dataPath + "/JsonData/ModelPath.json"));

        ModelList = new List<GameObject>();
        ModelNameList = new List<string>();

        foreach (var list in LoadPath.PathList)
        {
            string assetPath = "Assets/_Resource/Monster/Model/" + list.FileNames + ".prefab"; // JSON ���Ͽ��� ���� ��θ� �޾ƿ�
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

            if (prefab != null)
            {
                ModelNameList.Add(list.ModelName);
                ModelList.Add(prefab);
                Debug.Log("Prefab loaded and added to list: " + list.ModelName);
            }
            else
            {
                Debug.LogError("Failed to load prefab at path: " + assetPath);
                return;
            }
        }

        Debug.Log("�������� ��� �����Ͽ����ϴ�.");
    }

    public GameObject CheckList(string modelName)
    {
        if (ModelNameList.Contains(modelName))
        {
            int index = ModelNameList.IndexOf(modelName);
            if (index != -1 && index < ModelList.Count)
            {
                return ModelList[index];
            }

        }
        Debug.LogError("�׷� �̸��� ���� ���� �����ϴ�.");
        return null;
    }
}
