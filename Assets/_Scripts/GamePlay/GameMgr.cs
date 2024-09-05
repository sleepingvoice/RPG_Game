using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using BaseClass;

public class GameMgr : MonoBehaviour
{
    #region 싱글톤

    private static GameMgr instance = null;
    public static GameMgr ins
    {
        get
        {
            return instance;
        }
    }

    #endregion

    [SerializeField]private GameProgress GameProgressValue = GameProgress.Title;

    public BeventHandler LoginEvent = new BeventHandler();
    public BeventHandler LoadDataEvent = new BeventHandler();
    public BeventHandler LodingEvent = new BeventHandler();

    public SceneMgr SecneMgr;

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

    private void Start()
    {
        LoginEvent.CompletedEvent += () => BLog.ProgressLog("Login 이벤트 종료");
        LoginEvent.CompletedEvent += LoadData;

        LoadDataEvent.CompletedEvent += () => BLog.ProgressLog("Login 이벤트 종료");
        LoadDataEvent.CompletedEvent += Loading;


        LodingEvent.CompletedEvent += () => BLog.ProgressLog("Login 이벤트 종료");
    }

    public void Login()
    {
        BLog.ProgressLog("Login 이벤트 실행");
        GameProgressValue = GameProgress.Login;
        LoginEvent.InvokeEvent();
    }

    public void LoadData()
    {
        BLog.ProgressLog("LoadData 이벤트 실행");
        GameProgressValue = GameProgress.LoadData;
        LoadDataEvent.InvokeEvent();
    }

    public void Loading()
    {
        BLog.ProgressLog("LodingEvent 이벤트 실행");
        GameProgressValue = GameProgress.Loding;
        LodingEvent.InvokeEvent();
    }

}
