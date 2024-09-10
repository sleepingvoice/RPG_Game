using BaseClass;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataMgr : MonoBehaviour
{
    private Dictionary<string, string> titleData = new Dictionary<string, string>();
    public Dictionary<string, string> TitleData
    {
        get
        {
            return titleData;
        }
    }

    [SerializeField] private SO_Monster MonsterInfo;
    [SerializeField] private SO_SpawnMap MapSpawnInfo;

    private void Start()
    {
        GameMgr.ins.LoadDataEvent.OnEvent += LoadTitleData;
    }

    private void LoadTitleData()
    {

        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
        result =>
        {
            if (result.Data == null)
                Debug.Log("No TitleData");
            else
            {
                titleData = result.Data;
                /*
                foreach (var value in titleData)
                {
                    Debug.Log("키 : " + value.Key);
                    Debug.Log("밸류 : " + value.Value);
                }
                */
                LoadDataObject();
            }
        },
        error =>
        {
            Debug.Log("Got error getting titleData:");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    private void LoadDataObject()
    {
        LoadMonster();
        LoadMapSpawn();
        GameMgr.ins.LoadDataEvent.FinishEvent();
    }

    private void LoadMonster()
    {
        BdataDic<MonsterInfo> TestDic = new BdataDic<MonsterInfo>();
        MonsterInfo.monsterDic = TestDic.JsonToDic(TitleData["Monster"]);
#if UNITY_EDITOR
        MonsterInfo.UpdateInspectorDisplay();
#endif

        Debug.Log("몬스터 정보 불러오기 완료");
    }

    private void LoadMapSpawn()
    {
        BdataDic<MapInfo> TestDic2 = new BdataDic<MapInfo>();
        MapSpawnInfo.mapSpawnDic = TestDic2.JsonToDic(TitleData["MapSpawn"]);
#if UNITY_EDITOR
        MapSpawnInfo.UpdateInspectorDisplay();
#endif

        Debug.Log("맵 정보 불러오기 완료");
    }
}
