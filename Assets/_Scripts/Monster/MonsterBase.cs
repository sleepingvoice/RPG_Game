using BaseClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.Collections;
using System;
using static UnityEngine.GraphicsBuffer;

//몬스터 기본 기능
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
    /// 몬스터의 고유 Id를 부여하고 정보를 입력하여 초기 세팅을 함
    /// </summary>
    public void InitMonster(string SPMonsterID,MonsterInfo Info, GameObject ModelPrefab)
    {
        spawnID = SPMonsterID;
        info = Info;
        var MonsterPrefab = Instantiate(ModelPrefab, modelObj.transform);

        if (aiAgent = MonsterPrefab.GetComponent<NavMeshAgent>()) // nevmeshAget를 등록시킨다.
        {
            aiAgent = MonsterPrefab.AddComponent<NavMeshAgent>(); // 없으면 추가

            var ColSize = MonsterPrefab.GetComponent<BoxCollider>().size;
            aiAgent.radius = Mathf.Min(ColSize.x, ColSize.y) / 2; // 앞뒤와 좌우중 짧은족을 기준
            aiAgent.height = ColSize.y; // 높이를 기준
        }

        SetPos();
        MonsterPrefab.transform.position = moveTargetPos + Vector3.up * MonsterPrefab.GetComponent<BoxCollider>().size.y;
    }

    public void ChangeState() // 행동변경
    {
        StopCoroutine(nowStateCo); // 현재실행중인 코루틴을 멈춘다.
        nowStateCo = null;
    }



    IEnumerator MoveArround(Vector3 TargetPos)
    {
        while (Vector3.Distance(transform.position, TargetPos) > 0.1f)
        {
            yield return null;
        }

        ms_State.moveState = MS_Move.normal;
        yield return new WaitForSeconds(WaitTime); // 일정기간동안 대기

        //새로 목적지 설정
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
            Debug.LogError("AI가없습니다.");
            return;
        }

        moveTargetPos = TargetPos;
        aiAgent.SetDestination(TargetPos);
    }

    #region 맵위의 좌표 랜덤 선정
    public void SetPos()
    {
        while (true)
        {
            Vector3 xzPoint = new Vector3(UnityEngine.Random.Range(TargetMap.bounds.min.x, TargetMap.bounds.max.x), 0, UnityEngine.Random.Range(TargetMap.bounds.min.z, TargetMap.bounds.max.z));

            if (FindNavMeshY(xzPoint, out moveTargetPos))
                return;
        }
    }

    private bool FindNavMeshY(Vector3 xzPoint, out Vector3 result) // 내가 원하는 지점의 좌표가 있는지 체크
    {
        float maxHeight = TargetMap.bounds.max.y;
        float minHeight = TargetMap.bounds.min.y;

        // 시작 높이를 지정, 예를 들어 지형의 최대 높이 또는 기대하는 범위 내에서 조정 가능
        float height = maxHeight;

        // NavMesh.SamplePosition을 호출하여 해당 X,Z 좌표에서 Y 값을 찾습니다.
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
