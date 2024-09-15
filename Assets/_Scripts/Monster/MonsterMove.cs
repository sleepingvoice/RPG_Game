using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour
{
    [Tooltip("몬스터가 대기하는 시간")]
    [SerializeField] private float waitTime;

    [Tooltip("몬스터가 이동하는 범위제한")]
    [SerializeField] private int moveRange;


    private Vector3 standardPos; // 기준 좌표

    private Coroutine nowStateCo;
    private NavMeshAgent aiAgent;

    private Vector3 moveTargetPos;


    public void SetMoveState(Vector3 FirstPos, NavMeshAgent TargetAI)
    {
        standardPos = FirstPos;
        aiAgent = TargetAI;
    }

    /// <summary>
    /// 주변 돌아다니는 코루틴
    /// </summary>
    public IEnumerator MoveArround(Vector3 TargetPos)
    {
        while (Vector3.Distance(transform.position, TargetPos) > 0.1f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(waitTime); // 일정기간동안 대기

        //새로 목적지 설정
        SetPos();

        MoveToTarget(moveTargetPos);
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
            Vector3 xzPoint = new Vector3(UnityEngine.Random.Range(standardPos.x - moveRange, standardPos.x + moveRange), 0, UnityEngine.Random.Range(standardPos.z - moveRange, standardPos.z + moveRange));

            if (FindNavMeshY(xzPoint, out moveTargetPos))
                return;
        }
    }

    private bool FindNavMeshY(Vector3 xzPoint, out Vector3 result) // 내가 원하는 지점의 좌표가 있는지 체크
    {
        
        // NavMesh.SamplePosition을 호출하여 해당 X,Z 좌표에서 Y 값을 찾습니다.
        NavMeshHit hit;
        if (NavMesh.SamplePosition(new Vector3(xzPoint.x, 0, xzPoint.z), out hit, 10, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    #endregion

}
