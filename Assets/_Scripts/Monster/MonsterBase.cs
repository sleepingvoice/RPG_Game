using BaseClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.Collections;
using System;
using static UnityEngine.GraphicsBuffer;

//���� �⺻ ���
public class MonsterBase : MonoBehaviour
{
    [SerializeField] private string spawnID;
    [SerializeField] private MonsterInfo info;
    [SerializeField] private GameObject modelObj;
    [SerializeField] private float WaitTime;
    [SerializeField] private BoxCollider TargetMap;

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
    public void InitMonster(string SPMonsterID,MonsterInfo Info, GameObject ModelPrefab)
    {
        spawnID = SPMonsterID;
        info = Info;
        var MonsterPrefab = Instantiate(ModelPrefab, modelObj.transform);

        if (aiAgent = MonsterPrefab.GetComponent<NavMeshAgent>()) // nevmeshAget�� ��Ͻ�Ų��.
        {
            aiAgent = MonsterPrefab.AddComponent<NavMeshAgent>(); // ������ �߰�

            var ColSize = MonsterPrefab.GetComponent<BoxCollider>().size;
            aiAgent.radius = Mathf.Min(ColSize.x, ColSize.y) / 2; // �յڿ� �¿��� ª������ ����
            aiAgent.height = ColSize.y; // ���̸� ����
        }

        SetPos();
        MonsterPrefab.transform.position = moveTargetPos + Vector3.up * MonsterPrefab.GetComponent<BoxCollider>().size.y;
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
        yield return new WaitForSeconds(WaitTime); // �����Ⱓ���� ���

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
            Vector3 xzPoint = new Vector3(UnityEngine.Random.Range(TargetMap.bounds.min.x, TargetMap.bounds.max.x), 0, UnityEngine.Random.Range(TargetMap.bounds.min.z, TargetMap.bounds.max.z));

            if (FindNavMeshY(xzPoint, out moveTargetPos))
                return;
        }
    }

    private bool FindNavMeshY(Vector3 xzPoint, out Vector3 result) // ���� ���ϴ� ������ ��ǥ�� �ִ��� üũ
    {
        float maxHeight = TargetMap.bounds.max.y;
        float minHeight = TargetMap.bounds.min.y;

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
