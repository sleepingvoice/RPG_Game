using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public void Test2()
    {
        SceneManager.LoadScene("Map_01");
    }

    public void Test()
    {
        LoadSceneWithLoadingScreen("Map_01", "Loading");
    }

    public void LoadSceneWithLoadingScreen(string targetScene, string loadingScene)
    {
        StartCoroutine(LoadSceneAsync(targetScene, loadingScene));
    }

    private IEnumerator LoadSceneAsync(string targetScene, string loadingScene)
    {
        //�ε� ���� �ε�
        SceneManager.LoadScene(loadingScene);

        // Ÿ�� ���� �񵿱������ �ε�
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        // Ÿ�� ���� �ε�Ǵ� ���� �ε� ���� ǥ�� ���� ó��
        while (!asyncLoad.isDone)
        {
            // �ε��� ���� �������� (progress ���� 0.9���� ����)
            if (asyncLoad.progress >= 0.9f)
            {
                // �ε��� �Ϸ�Ǿ����� �˸��� UI ó�� ����
                // ���⼭ �߰����� ������ ��ٸ� �� ���� (��: ��ư Ŭ�� ��)
                // ���� �� �ε� UI���� "Tap to Continue" ��...

                // Ÿ�� �� Ȱ��ȭ
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        // �ε� �� ��ε�
        yield return SceneManager.UnloadSceneAsync(loadingScene);
    }
}
