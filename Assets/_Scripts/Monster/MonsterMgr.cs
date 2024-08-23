using BaseClass;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

//���� �Ŵ���
public class MonsterMgr : MonoBehaviour
{
    private Dictionary<string, MonsterInfo> monsterDic;

    [SerializeField] private List<string> ModelName;
    [SerializeField] private List<GameObject> MonsterModel;

    private void Start()
    {
        GameMgr.Instance.InitGameEvent += LoadMonsterDic;
    }

    public void LoadMonsterDic()
    {
        BdataDic<MonsterInfo> TestDic = new BdataDic<MonsterInfo>();
        monsterDic = TestDic.JsonToDic(GameMgr.Instance.TitleData["Monster"]);

        Debug.Log("���� ���� �ҷ����� �Ϸ�");
    }

    #region ���� �� �ε�

    public void LoadMonsterModelPath()
    {
        PathListClass LoadPath = JsonUtility.FromJson<PathListClass>(File.ReadAllText(Application.dataPath + "/JsonData/ModelPath.json"));

        MonsterModel = new List<GameObject>();
        ModelName = new List<string>();

        foreach (var list in LoadPath.PathList)
        {
            string assetPath = "Assets/_Resource/Monster/Model/" + list.FileNames + ".prefab"; // JSON ���Ͽ��� ���� ��θ� �޾ƿ�
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

            if (prefab != null)
            {
                ModelName.Add(list.ModelName);
                MonsterModel.Add(prefab);
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
    
    #endregion
}
