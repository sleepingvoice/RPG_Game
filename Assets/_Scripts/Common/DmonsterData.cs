using UnityEngine;
using BaseSystem;
using System.Collections.Generic;

namespace DataSystem
{
    public class MonsterInfo
    {
        private Bcharacter monsterCharacter;
        private float attackSpeed;
        private float moveSpeed;

    }

    public class MmonsterData
    {
        public Dictionary<string, MonsterInfo> monsterInfoDic = new Dictionary<string, MonsterInfo>();
    }
}
