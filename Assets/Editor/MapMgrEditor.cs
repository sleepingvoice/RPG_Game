using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapMgr))]
public class MapMgrEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �⺻ �ν����� �����
        DrawDefaultInspector();

        // MonsterMgr Ÿ��
        MapMgr MapMgr = (MapMgr)target;

        // ��ư �߰�
        if (GUILayout.Button("Load Map List"))
        {
            // LoadMonsterModelPath �Լ� ȣ��
            MapMgr.LoadMapName();
        }
    }
}
