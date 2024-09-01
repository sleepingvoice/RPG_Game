using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_ModelAddress))]
public class ModelAdressEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 드로잉
        DrawDefaultInspector();

        // MonsterMgr 타겟
        SO_ModelAddress Model = (SO_ModelAddress)target;

        // 버튼 추가
        if (GUILayout.Button("Load Monster Models"))
        {
            // LoadMonsterModelPath 함수 호출
            Model.LoadMonsterModelPath();
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
    }
}
