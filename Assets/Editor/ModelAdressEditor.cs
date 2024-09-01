using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_ModelAddress))]
public class ModelAdressEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �⺻ �ν����� �����
        DrawDefaultInspector();

        // MonsterMgr Ÿ��
        SO_ModelAddress Model = (SO_ModelAddress)target;

        // ��ư �߰�
        if (GUILayout.Button("Load Monster Models"))
        {
            // LoadMonsterModelPath �Լ� ȣ��
            Model.LoadMonsterModelPath();
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
    }
}
