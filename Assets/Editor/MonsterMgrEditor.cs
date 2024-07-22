using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterMgr))]
public class MonsterMgrEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 드로잉
        DrawDefaultInspector();

        // MonsterMgr 타겟
        MonsterMgr monsterMgr = (MonsterMgr)target;

        // 버튼 추가
        if (GUILayout.Button("Load Monster Models"))
        {
            // LoadMonsterModelPath 함수 호출
            monsterMgr.LoadMonsterModelPath();
        }
    }
}
