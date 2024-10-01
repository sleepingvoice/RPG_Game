using BaseClass;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//���� �⺻ ���
public class MonsterBase : BcharBase
{
    [Tooltip("���� ID")]
    [SerializeField] private string spawnID;

    [Tooltip("���� ����")]
    [SerializeField] private MonsterInfo info;

    [Tooltip("���� ���� �θ� ��ü")]
    [SerializeField] private GameObject modelObj;


    private Rigidbody rigid;
    private Coroutine nowStateCo;
    private NavMeshAgent aiAgent;

    private void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
        nowStateCo = null;
    }

    /// <summary>
    /// ������ ���� Id�� �ο��ϰ� ������ �Է��Ͽ� �ʱ� ������ ��
    /// </summary>
    public void InitMonster(string SPMonsterID, MonsterInfo Info, GameObject MonsterModle, BoxCollider TargetMap)
    {
        spawnID = SPMonsterID;
        info = Info;
        var ModelPrefab = Instantiate(MonsterModle, modelObj.transform);

        if (aiAgent = ModelPrefab.GetComponent<NavMeshAgent>()) // nevmeshAget�� ��Ͻ�Ų��.
        {
            aiAgent = ModelPrefab.AddComponent<NavMeshAgent>(); // ������ �߰�

            var ColSize = ModelPrefab.GetComponent<BoxCollider>().size;
            aiAgent.radius = Mathf.Min(ColSize.x, ColSize.y) / 2; // �յڿ� �¿��� ª������ ����
            aiAgent.height = ColSize.y; // ���̸� ����
        }
      

    }

    public void ChangeState() // �ൿ����
    {
        StopCoroutine(nowStateCo); // ����������� �ڷ�ƾ�� �����.
        nowStateCo = null;
    }



}
