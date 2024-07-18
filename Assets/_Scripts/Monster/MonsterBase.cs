using BaseClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� �⺻ ���
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
    /// ������ ���� Id�� �ο��ϰ� ������ �Է��Ͽ� �ʱ� ������ ��
    /// </summary>
    public void InitMonster(string SPMonsterID,MonsterInfo Info)
    {
        spawnID = SPMonsterID;
        info = Info;
    }
}
