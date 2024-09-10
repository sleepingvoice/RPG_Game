using BaseClass;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//���� �⺻ ���
public class MonsterBase : BCharBase
{
    [Tooltip("���� ID")]
    [SerializeField] private string spawnID;

    [Tooltip("���� ����")]
    [SerializeField] private MonsterInfo info;

    [Tooltip("���� ���� �θ� ��ü")]
    [SerializeField] private GameObject modelObj;

    [Tooltip("���Ͱ� ����ϴ� �ð�")]
    [SerializeField] private float waitTime;

    [Tooltip("���Ͱ� ��ȯ�� ���� �ݶ��̴�")]
    [SerializeField] private BoxCollider targetMap;

    private Vector3 moveTargetPos;

    private MonsterState ms_State = new MonsterState();
    private Rigidbody rigid;
    private Collider col;
    private Coroutine nowStateCo;
    private NavMeshAgent aiAgent;

    private void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
        col = this.GetComponent<Collider>();
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

        SetPos();
        ModelPrefab.transform.position = moveTargetPos + Vector3.up * ModelPrefab.GetComponent<BoxCollider>().size.y;
    }

    public void ChangeState() // �ൿ����
    {
        StopCoroutine(nowStateCo); // ����������� �ڷ�ƾ�� �����.
        nowStateCo = null;
    }

    IEnumerator MoveArround(Vector3 TargetPos)
    {
        while (Vector3.Distance(transform.position, TargetPos) > 0.1f)
        {
            yield return null;
        }

        ms_State.moveState = MS_Move.normal;
        yield return new WaitForSeconds(waitTime); // �����Ⱓ���� ���

        //���� ������ ����
        SetPos();

        MoveToTarget(moveTargetPos);
        ms_State.moveState = MS_Move.walk;
        MoveArround(moveTargetPos);
        yield return null;
    }

    public void MoveToTarget(Vector3 TargetPos)
    {
        if (!aiAgent)
        {
            Debug.LogError("AI�������ϴ�.");
            return;
        }

        moveTargetPos = TargetPos;
        aiAgent.SetDestination(TargetPos);
    }

    #region ������ ��ǥ ���� ����
    public void SetPos()
    {
        while (true)
        {
            Vector3 xzPoint = new Vector3(UnityEngine.Random.Range(targetMap.bounds.min.x, targetMap.bounds.max.x), 0, UnityEngine.Random.Range(targetMap.bounds.min.z, targetMap.bounds.max.z));

            if (FindNavMeshY(xzPoint, out moveTargetPos))
                return;
        }
    }

    private bool FindNavMeshY(Vector3 xzPoint, out Vector3 result) // ���� ���ϴ� ������ ��ǥ�� �ִ��� üũ
    {
        float maxHeight = targetMap.bounds.max.y;
        float minHeight = targetMap.bounds.min.y;

        // ���� ���̸� ����, ���� ��� ������ �ִ� ���� �Ǵ� ����ϴ� ���� ������ ���� ����
        float height = maxHeight;

        // NavMesh.SamplePosition�� ȣ���Ͽ� �ش� X,Z ��ǥ���� Y ���� ã���ϴ�.
        NavMeshHit hit;
        if (NavMesh.SamplePosition(new Vector3(xzPoint.x, height, xzPoint.z), out hit, maxHeight - minHeight, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    #endregion

}
