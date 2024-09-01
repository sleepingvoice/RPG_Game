using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterSettingMgr))]
public class MonsterSetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �⺻ �ν����� �����
        DrawDefaultInspector();

        // MonsterMgr Ÿ��
        MonsterSettingMgr Model = (MonsterSettingMgr)target;

        // ��ư �߰�
        if (GUILayout.Button("CreatePrefabs"))
        {
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null)
            {
                // SceneView ī�޶��� ��ġ�� ȸ�� ���� ��������
                Vector3 cameraPosition = sceneView.camera.transform.position + sceneView.camera.transform.forward * 5;
                Model.CreatePrefabModel(cameraPosition);

                Debug.Log("���������� �����Ͽ����ϴ�.");
            }
        }


        if (GUILayout.Button("CheckList"))
        {
            Model.ListCheck();
        }

        if (GUILayout.Button("ClearObject"))
        {
            Model.ClearList();
        }
    }
}

