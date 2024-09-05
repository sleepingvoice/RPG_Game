using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using BaseClass;

public class GameMgr : MonoBehaviour
{
    #region �̱���

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
        LoginEvent.CompletedEvent += () => BLog.ProgressLog("Login �̺�Ʈ ����");
        LoginEvent.CompletedEvent += LoadData;

        LoadDataEvent.CompletedEvent += () => BLog.ProgressLog("Login �̺�Ʈ ����");
        LoadDataEvent.CompletedEvent += Loading;


        LodingEvent.CompletedEvent += () => BLog.ProgressLog("Login �̺�Ʈ ����");
    }

    public void Login()
    {
        BLog.ProgressLog("Login �̺�Ʈ ����");
        GameProgressValue = GameProgress.Login;
        LoginEvent.InvokeEvent();
    }

    public void LoadData()
    {
        BLog.ProgressLog("LoadData �̺�Ʈ ����");
        GameProgressValue = GameProgress.LoadData;
        LoadDataEvent.InvokeEvent();
    }

    public void Loading()
    {
        BLog.ProgressLog("LodingEvent �̺�Ʈ ����");
        GameProgressValue = GameProgress.Loding;
        LodingEvent.InvokeEvent();
    }

}
