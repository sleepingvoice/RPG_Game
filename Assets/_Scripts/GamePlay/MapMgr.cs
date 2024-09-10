using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class MapMgr : MonoBehaviour
    {
        public void SceneLoad(string MapName)
        {
            StartCoroutine(LoadScene(MapName));
        }
        public void SceneUnLoad(string MapName)
        {
            StartCoroutine(UnLoadScene(MapName));
        }

        IEnumerator LoadScene(string MapName)
        {
            if (SceneManager.GetSceneByName(MapName).IsValid())
                yield return SceneManager.LoadSceneAsync(MapName, LoadSceneMode.Additive);
            else
                Debug.Log("씬이 존재하지 않습니다");

            yield return null;
        }
        IEnumerator UnLoadScene(string MapName)
        {
            if (SceneManager.GetSceneByName(MapName).isLoaded)
                yield return SceneManager.UnloadSceneAsync(MapName);
            else
                Debug.Log("플레이중인 씬이 아닙니다.");

            yield return null;
        }
    }
}
