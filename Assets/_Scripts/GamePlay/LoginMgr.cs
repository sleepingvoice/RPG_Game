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

    #region playFab �α���
    private void Login()
    {
        var request = new LoginWithPlayFabRequest { Username = loginId, Password = loginPass };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("�α��� ����");
        entityId = result.EntityToken.Entity.Id;
        entityType = result.EntityToken.Entity.Type;

        GameMgr.ins.LoginEvent.FinishEvent();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("�α��� ����");
        Debug.LogWarning(error.GenerateErrorReport());
    }
    #endregion
}
