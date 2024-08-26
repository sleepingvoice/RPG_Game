using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class GameMgr : MonoBehaviour
{
    private static GameMgr instance = null;
    public static GameMgr Instance
    {
        get
        {
            return instance;
        }
    }

    private Dictionary<string, string> titleData = new Dictionary<string, string>();
    public Dictionary<string, string> TitleData { get { return TitleData; } }

    public event Action InitGameEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void initGame()
    {
        InitGameEvent?.Invoke();
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
