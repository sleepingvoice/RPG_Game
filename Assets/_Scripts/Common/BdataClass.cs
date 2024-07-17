using System;
using System.Collections;
using System.Collections.Generic;

namespace BaseClass
{
    [Serializable]
    public class CharInfo
    {
        // 외부에서 값 변경을 어렵게하기위해 사용
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