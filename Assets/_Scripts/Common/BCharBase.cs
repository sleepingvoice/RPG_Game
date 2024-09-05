using BaseClass;
using UnityEngine;

public class BCharBase : MonoBehaviour
{
    protected CharInfo baseCharInfo;

    public void Dameged(int Damage)
    {
        baseCharInfo.nowHp -= Damage;
    }
}
