using System;
using System.Collections.Generic;

namespace BaseClass
{
    [Serializable]
    public class CharInfo
    {
        // �ܺο��� �� ������ ��ư��ϱ����� ���
        public int hp;
        public int atk;
        public int def;
        public string modelPath;
    }

    [Serializable]
    public class MonsterInfo
    {
        public string monsterID;
        public CharInfo monsterChar;
        public int attackSpeed;
        public int moveSpeed;
        public bool firstAttack; // ����
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
    public class MapInfo
    {
        public string mapID;
        public string mapName;
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

}