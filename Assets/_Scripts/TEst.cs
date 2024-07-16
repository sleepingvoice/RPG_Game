using PlayFab;
using PlayFab.ClientModels;
using System.Linq;
using UnityEngine;

public class TEst : MonoBehaviour
{
    static string customId = "";
    static string playfabId = "";

    private string entityId;
    private string entityType;

    // Start is called before the first frame update
    void Start()
    {
        customId = GetRandomPassword(16);
    }

    public void Login()
    {
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CustomId = customId,
            CreateAccount = true
        }, result =>
        {
            OnLoginSuccess(result);
            ClientGetTitleData();
        }, error =>
        {
            Debug.LogError("Login Fail - Guest");
        });
    }

    public void OnLoginSuccess(LoginResult result) //로그인 결과
    {
        Debug.Log("Playfab Login Success");

        playfabId = result.PlayFabId;
        entityId = result.EntityToken.Entity.Id;
        entityType = result.EntityToken.Entity.Type;
    }

    private string GetRandomPassword(int _totLen) //랜덤한 16자리 id 생성
    {
        string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var chars = Enumerable.Range(0, _totLen)
            .Select(x => input[UnityEngine.Random.Range(0, input.Length)]);
        return new string(chars.ToArray());
    }

    //데이터 불러오기 (로그인을 해야 불러올수 있음)
    public void ClientGetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
            result => {
                if (result.Data == null || !result.Data.ContainsKey("MonsterName")) Debug.Log("No MonsterName");
                else Debug.Log("MonsterName: " + result.Data["MonsterName"]);
            },
            error => {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }
}
