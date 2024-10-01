using BaseClass;
using UnityEngine;

public class BcharBase : MonoBehaviour
{
    protected CharInfo baseCharInfo;

    public void Dameged(int Damage)
    {
        baseCharInfo.nowHp -= Damage;
    }
}
