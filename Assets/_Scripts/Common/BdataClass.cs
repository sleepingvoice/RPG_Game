using System;
using System.Collections;
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
        public string monstID;
        public CharInfo monsterChar;
        public int attackSpeed;
        public int moveSpeed;
    }

    [Serializable]
    public class PlayerInfo
    {
        public string userID;
        public CharInfo playerChar;
        public int level;
        public int sp;
    }
}