using BaseClass;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{
    static string loginId = "Test";
    static string loginPass = "123456";

    private string entityId;
    private string entityType;

    [Serializable]
    public class LoadDataClass
    {
        public string id;
        public MonsterInfo info;
    }

    [Serializable]
    public class LoadDataList
    {
        public List<LoadDataClass> List;
    }

    public LoadDataList ShowLoadData = new LoadDataList();

    // Start is called before the first frame update
    void Start()
    {
    }

    public void TestJsonDebug()
    {
        LoadDataList test = new LoadDataList();
        test.List = new List<LoadDataClass>();

        LoadDataClass Test1 = new LoadDataClass();
        Test1.id = "0001";
        MonsterInfo TestInfo1 = new MonsterInfo();
        TestInfo1.attackSpeed = 1;
        TestInfo1.monsterID = "0001";
        TestInfo1.moveSpeed = 10;
        CharInfo Testchar1 = new CharInfo();
        Testchar1.hp = 100;
        Testchar1.atk = 10;
        Testchar1.def = 10;
        Testchar1.modelPath = "대충경로";
        TestInfo1.monsterChar = Testchar1;
        Test1.info = TestInfo1;
        test.List.Add(Test1);

        LoadDataClass Test2 = new LoadDataClass();
        Test2.id = "0002";
        MonsterInfo TestInfo2 = new MonsterInfo();
        TestInfo2.attackSpeed = 2;
        TestInfo2.monsterID = "0002";
        TestInfo2.moveSpeed = 5;
        CharInfo Testchar2 = new CharInfo();
        Testchar2.hp = 50;
        Testchar2.atk = 5;
        Testchar2.def = 5;
        Testchar2.modelPath = "대충경로";
        TestInfo2.monsterChar = Testchar2;
        Test2.info = TestInfo2;
        test.List.Add(Test2);

        LoadDataClass Test3 = new LoadDataClass();
        Test3.id = "0003";
        MonsterInfo TestInfo3 = new MonsterInfo();
        TestInfo3.attackSpeed = 1;
        TestInfo3.monsterID = "0003";
        TestInfo3.moveSpeed = 20;
        CharInfo Testchar3 = new CharInfo();
        Testchar3.hp = 200;
        Testchar3.atk = 20;
        Testchar3.def = 20;
        Testchar3.modelPath = "대충경로";
        TestInfo3.monsterChar = Testchar3;
        Test3.info = TestInfo3;
        test.List.Add(Test3);

        Debug.Log(JsonUtility.ToJson(test));
    }

    #region playFab 로그인
    public void Login()
    {
        var request = new LoginWithPlayFabRequest { Username = loginId, Password = loginPass };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
        entityId = result.EntityToken.Entity.Id;
        entityType = result.EntityToken.Entity.Type;
        ClientGetTitleData();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("로그인 실패");
        Debug.LogWarning(error.GenerateErrorReport());
    }

    #endregion

    #region playFab 회원가입
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Username = "Test", Password = "123456", Email = "Test@naver.com" };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("가입 성공");
    }

    private void RegisterFailure(PlayFabError error)
    {
        Debug.LogWarning("가입 실패");
        Debug.LogWarning(error.GenerateErrorReport());
    }

    #endregion

    #region Player Fab TitleData불러오기

    BdataDic<MonsterInfo> TestDic = new BdataDic<MonsterInfo>();
    //데이터 불러오기 (로그인을 해야 불러올수 있음)
    public void ClientGetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
        result => {
            if (result.Data == null || !result.Data.ContainsKey("Monster"))
                Debug.Log("No MonsterName");
            else
            {
                //ShowLoadData = JsonUtility.FromJson<LoadDataList>(result.Data["Monster"]);
                var dic = TestDic.JsonToDic(result.Data["Monster"]);
                ShowLoadData.List = new List<LoadDataClass>();
                foreach (var dicvalue in dic)
                {
                    LoadDataClass Tmp = new LoadDataClass();
                    Tmp.id = dicvalue.Key;
                    Tmp.info = dicvalue.Value;
                    ShowLoadData.List.Add(Tmp);
                }
            }
            },
            error => {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }

    #endregion

}
