using BaseClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr : MonoBehaviour
{
    private Dictionary<string, MapInfo> mapDic;

    private void Start()
    {
        GameMgr.Instance.InitGameEvent += LoadMapDic;
    }

    public void LoadMapDic()
    {
        BdataDic<MapInfo> TestDic = new BdataDic<MapInfo>();
        mapDic = TestDic.JsonToDic(GameMgr.Instance.TitleData["Map"]);


        Debug.Log("맵 정보 불러오기 완료");
    }
}
