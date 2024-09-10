using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class LoginMgr : MonoBehaviour
{
    static string loginId = "Test";
    static string loginPass = "123456";

    private string entityId;
    private string entityType;

    private void Start()
    {
        GameMgr.ins.LoginEvent.OnEvent += Login;
    }

    private void OnDestroy()
    {
        GameMgr.ins.LoginEvent.OnEvent -= Login;
    }

    #region playFab 로그인
    private void Login()
    {
        var request = new LoginWithPlayFabRequest { Username = loginId, Password = loginPass };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
        entityId = result.EntityToken.Entity.Id;
        entityType = result.EntityToken.Entity.Type;

        GameMgr.ins.LoginEvent.FinishEvent();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("로그인 실패");
        Debug.LogWarning(error.GenerateErrorReport());
    }
    #endregion
}
