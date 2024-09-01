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
            //load씬의 Loging Bar를 이동시키는 작업
            //바가 끝나면 while 종료시킴

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
