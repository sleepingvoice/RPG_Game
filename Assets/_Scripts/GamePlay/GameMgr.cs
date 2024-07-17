using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class GameMgr
{
    private static GameMgr instance = null;
    public static GameMgr Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new GameMgr();
            }
            return instance;
        }
    }

    private Dictionary<string, string> titleData = new Dictionary<string, string>();
    public Dictionary<string, string> TitleData { get { return TitleData; } }

    public void initGame()
    {

    }

    public void LoadTitleData()
    {
        
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
        result =>
        {
            if (result.Data == null)
                Debug.Log("No TitleData");
            else
                titleData = result.Data;
        },
        error =>
        {
            Debug.Log("Got error getting titleData:");
            Debug.Log(error.GenerateErrorReport());
        });
    }

}
