using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapMgr))]
public class MapMgrEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 드로잉
        DrawDefaultInspector();

        // MonsterMgr 타겟
        MapMgr MapMgr = (MapMgr)target;

        // 버튼 추가
        if (GUILayout.Button("Load Map List"))
        {
            // LoadMonsterModelPath 함수 호출
            MapMgr.LoadMapName();
        }
    }
}
