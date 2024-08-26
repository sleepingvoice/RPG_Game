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
        //로딩 씬을 로드
        SceneManager.LoadScene(loadingScene);

        // 타겟 씬을 비동기식으로 로드
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        // 타겟 씬이 로드되는 동안 로딩 상태 표시 등을 처리
        while (!asyncLoad.isDone)
        {
            // 로딩이 거의 끝났으면 (progress 값은 0.9에서 멈춤)
            if (asyncLoad.progress >= 0.9f)
            {
                // 로딩이 완료되었음을 알리는 UI 처리 가능
                // 여기서 추가적인 조건을 기다릴 수 있음 (예: 버튼 클릭 시)
                // 예를 들어서 로딩 UI에서 "Tap to Continue" 등...

                // 타겟 씬 활성화
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        // 로딩 씬 언로드
        yield return SceneManager.UnloadSceneAsync(loadingScene);
    }
}
