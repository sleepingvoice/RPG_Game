using UnityEngine;

namespace BaseSystem
{
    public class CharacterInfo
    {
        // 외부에서 값 변경을 어렵게하기위해 사용
        public int hp;
        public int atk;
        public int def;
        public string modelPath;
    }

    public class Bcharacter
    {
        protected CharacterInfo info;

        public Bcharacter(CharacterInfo CharacterInfo)
        {
            info = CharacterInfo;
        }
    }
}
