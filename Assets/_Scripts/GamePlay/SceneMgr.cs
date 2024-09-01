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
        SceneManager.LoadScene(loadingScene);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            //load���� Loging Bar�� �̵���Ű�� �۾�
            //�ٰ� ������ while �����Ŵ

            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void UnloadLodingScene(string loadingScene)
    {
        StartCoroutine(UnloadLodingSceneAsync(loadingScene));
    }

    private IEnumerator UnloadLodingSceneAsync(string loadingScene)
    {
        yield return SceneManager.UnloadSceneAsync(loadingScene);
    }
}
