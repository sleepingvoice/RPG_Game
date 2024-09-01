using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterSettingMgr))]
public class MonsterSetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 드로잉
        DrawDefaultInspector();

        // MonsterMgr 타겟
        MonsterSettingMgr Model = (MonsterSettingMgr)target;

        // 버튼 추가
        if (GUILayout.Button("CreatePrefabs"))
        {
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null)
            {
                // SceneView 카메라의 위치와 회전 정보 가져오기
                Vector3 cameraPosition = sceneView.camera.transform.position + sceneView.camera.transform.forward * 5;
                Model.CreatePrefabModel(cameraPosition);

                Debug.Log("성공적으로 생성하였습니다.");
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

