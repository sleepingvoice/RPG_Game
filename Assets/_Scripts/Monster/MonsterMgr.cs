using BaseClass;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

//몬스터 매니저
public class MonsterMgr : MonoBehaviour
{
    public MonsterBase monsterBase;


    private Dictionary<string, MonsterInfo> monsterDic;

    [SerializeField] private List<string> ModelName;
    [SerializeField] private List<GameObject> MonsterModel;

    private void Start()
    {
        GameMgr.Instance.InitGameEvent += LoadDic;
    }

    public void LoadDic()
    {
        BDataDic<MonsterInfo> TestDic = new BDataDic<MonsterInfo>();
        monsterDic = TestDic.JsonToDic(GameMgr.Instance.TitleData["Monster"]);
    }

    #region 몬스터 모델 로드

    public void LoadMonsterModelPath()
    {
        PathListClass LoadPath = JsonUtility.FromJson<PathListClass>(File.ReadAllText(Application.dataPath + "/JsonData/ModelPath.json"));

        MonsterModel = new List<GameObject>();
        ModelName = new List<string>();

        foreach (var list in LoadPath.PathList)
        {
            string assetPath = "Assets/_Resource/Monster/Model/" + list.FileNames + ".prefab"; // JSON 파일에서 파일 경로를 받아옴
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

        Debug.Log("프리펩을 모두 저장하였습니다.");
    }
    
    #endregion
}
