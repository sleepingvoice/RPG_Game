using System.Collections.Generic;
using UnityEngine;

//���� ������Ʈ Ǯ����
public class MonsterPool : MonoBehaviour
{
    [SerializeField] private MonsterBase monsterPrefab;
    [SerializeField] private int poolSize;

    private Queue<MonsterBase> activePool = new Queue<MonsterBase>();
    private Queue<MonsterBase> disactivePool = new Queue<MonsterBase>();

    /// <summary>
    /// ���� ����
    /// </summary>
    public MonsterBase GetMonster()
    {
        if (activePool.Count > 0)
        {
            var monster = disactivePool.Dequeue();
            monster.gameObject.SetActive(true);
            return monster;
        }
        else
        {
            // Ǯ�� ���Ͱ� ���� ��� ���� ����
            return CreateNewMonster();
        }
    }

    private MonsterBase CreateNewMonster()
    {
        var newMonster = Instantiate(monsterPrefab);
        newMonster.transform.SetParent(this.transform);
        newMonster.gameObject.SetActive(true);
        activePool.Enqueue(newMonster);
        return newMonster;
    }

    /// <summary>
    /// ���� ��Ȱ��ȭ
    /// </summary>
    public void ReturnMonster(MonsterBase monster)
    {
        if (activePool.Contains(monster))
        {
            monster.gameObject.SetActive(false);
            if (disactivePool.Count > poolSize)
                DestroyImmediate(monster);
            else
                disactivePool.Enqueue(monster);
        }
        else
            Debug.LogError(monster.name + "�̶�� �̸��� ���ʹ� ����Ǯ�� �����ϴ�.");
    }
}
