using BaseClass;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapMgr : MonoBehaviour
{
    private Dictionary<string, MapInfo> mapDic;

    [SerializeField] private List<string> MapName;

    #region 맵 이름 불러오기

    public void LoadMapName()
    {
        MapName.Clear();
        SceneNameList LoadPath = JsonUtility.FromJson<SceneNameList>(File.ReadAllText(Application.dataPath + "/JsonData/MapName.json"));
        foreach (var PathList in LoadPath.NameList)
        {
            MapName.Add(PathList);
        }
        
        Debug.Log("경로를 모두 저장하였습니다.");
    }

    #endregion
}
