using System.IO;
using UnityEditor;
using UnityEngine;
using BaseClass;

public class MapLoad : MonoBehaviour
{
    private const string saveFilePath = "/JsonData/MapName.json";

    [MenuItem("Window/Custom/MapLoad")]
    public static void GetAllScenesInBuild()
    {
        // Build Settings에 포함된 모든 씬의 경로를 가져옴
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

        SceneNameList SceneNameList = new SceneNameList();
        
        string result;

        // 각 씬의 이름을 출력
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            if (scene.enabled)
            {
                // 씬의 경로에서 이름만 추출
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
                SceneNameList.NameList.Add(sceneName);
            }
        }

        if (SceneNameList.NameList.Count != 0)
        {
            File.WriteAllText(Application.dataPath + saveFilePath, JsonUtility.ToJson(SceneNameList));
            result = "저장에 성공했습니다.";
        }
        else
            result = "저장에 실패했습니다.";

        EditorUtility.DisplayDialog("Failure File Path", result, "OK");
    }
}

