using UnityEngine;

namespace BaseSystem
{
    public class CharacterInfo
    {
        // �ܺο��� �� ������ ��ư��ϱ����� ���
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
