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
        // Build Settings�� ���Ե� ��� ���� ��θ� ������
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

        SceneNameList SceneNameList = new SceneNameList();
        
        string result;

        // �� ���� �̸��� ���
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            if (scene.enabled)
            {
                // ���� ��ο��� �̸��� ����
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
                SceneNameList.NameList.Add(sceneName);
            }
        }

        if (SceneNameList.NameList.Count != 0)
        {
            File.WriteAllText(Application.dataPath + saveFilePath, JsonUtility.ToJson(SceneNameList));
            result = "���忡 �����߽��ϴ�.";
        }
        else
            result = "���忡 �����߽��ϴ�.";

        EditorUtility.DisplayDialog("Failure File Path", result, "OK");
    }
}

