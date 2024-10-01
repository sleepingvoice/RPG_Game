using BaseClass;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//몬스터 기본 기능
public class MonsterBase : BcharBase
{
    [Tooltip("고유 ID")]
    [SerializeField] private string spawnID;

    [Tooltip("몬스터 정보")]
    [SerializeField] private MonsterInfo info;

    [Tooltip("몬스터 모델의 부모 객체")]
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
    /// 몬스터의 고유 Id를 부여하고 정보를 입력하여 초기 세팅을 함
    /// </summary>
    public void InitMonster(string SPMonsterID, MonsterInfo Info, GameObject MonsterModle, BoxCollider TargetMap)
    {
        spawnID = SPMonsterID;
        info = Info;
        var ModelPrefab = Instantiate(MonsterModle, modelObj.transform);

        if (aiAgent = ModelPrefab.GetComponent<NavMeshAgent>()) // nevmeshAget를 등록시킨다.
        {
            aiAgent = ModelPrefab.AddComponent<NavMeshAgent>(); // 없으면 추가

            var ColSize = ModelPrefab.GetComponent<BoxCollider>().size;
            aiAgent.radius = Mathf.Min(ColSize.x, ColSize.y) / 2; // 앞뒤와 좌우중 짧은족을 기준
            aiAgent.height = ColSize.y; // 높이를 기준
        }
      

    }

    public void ChangeState() // 행동변경
    {
        StopCoroutine(nowStateCo); // 현재실행중인 코루틴을 멈춘다.
        nowStateCo = null;
    }



}
