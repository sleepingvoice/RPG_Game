using BaseClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터 매니저
public class MonsterMgr : MonoBehaviour
{
    public MonsterBase monsterBase;


    [SerializeField] private Dictionary<string, MonsterInfo> monsterDic;


}
