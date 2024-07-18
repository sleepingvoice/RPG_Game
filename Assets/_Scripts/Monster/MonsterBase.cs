using BaseClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터 기본 기능
public class MonsterBase : MonoBehaviour
{
    [SerializeField] private string spawnID;
    [SerializeField] private MonsterInfo info;

    private Rigidbody rigid;
    private Collider col;

    private void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
        col = this.GetComponent<Collider>();
    }

    /// <summary>
    /// 몬스터의 고유 Id를 부여하고 정보를 입력하여 초기 세팅을 함
    /// </summary>
    public void InitMonster(string SPMonsterID,MonsterInfo Info)
    {
        spawnID = SPMonsterID;
        info = Info;
    }
}
