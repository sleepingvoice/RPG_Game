using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterMgr))]
public class MonsterMgrEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �⺻ �ν����� �����
        DrawDefaultInspector();

        // MonsterMgr Ÿ��
        MonsterMgr MonsterMgr = (MonsterMgr)target;

        // ��ư �߰�
        if (GUILayout.Button("Load Monster Models"))
        {
            // LoadMonsterModelPath �Լ� ȣ��
            MonsterMgr.LoadMonsterModelPath();
        }
    }
}
