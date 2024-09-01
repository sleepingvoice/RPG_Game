using System;
using System.Collections.Generic;
using System.Numerics;

namespace BaseClass
{
    [Serializable]
    public class CharInfo
    {
        // 외부에서 값 변경을 어렵게하기위해 사용
        public int hp;
        public int atk;
        public int def;
        public string modelName;
    }

    [Serializable]
    public class MonsterInfo
    {
        public string monsterID;
        public CharInfo monsterChar;
        public int attackSpeed;
        public int moveSpeed;
        public bool firstAttack; // 선공
    }

    public class MonsterState
    {
        public MS_Move moveState = MS_Move.normal;
        public MS_Attak attackState = MS_Attak.normal;
    }

    [Serializable]
    public class PlayerInfo
    {
        public string userID;
        public CharInfo playerChar;
        public int level;
        public int sp;
    }

    [Serializable]
    public class PathInfo
    {
        public string ModelName;
        public string FileNames;
    }

    [Serializable]
    public class PathListClass
    {
        public List<PathInfo> PathList;
    }

    [Serializable]
    public class SceneNameList
    {
        public List<string> NameList = new List<string>();
    }

    [Serializable]
    public class SpawnInfo
    {
        public string SpawnID;
        public string SpawnModelID;
        public Vector3 SpawnPos;
    }

    [Serializable]
    public class MapInfo
    {
        public string MapName;
        public List<SpawnInfo> SpawnList = new List<SpawnInfo>();
    }
}