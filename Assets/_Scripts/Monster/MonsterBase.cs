using BaseClass;
using UnityEngine;

//���� �⺻ ���
public class MonsterBase : MonoBehaviour
{
    [SerializeField] private string spawnID;
    [SerializeField] private MonsterInfo info;
    [SerializeField] private GameObject modelObj;

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
    public void InitMonster(string SPMonsterID,MonsterInfo Info, GameObject ModelPrefab)
    {
        spawnID = SPMonsterID;
        info = Info;
        Instantiate(ModelPrefab, modelObj.transform);
    }
}
