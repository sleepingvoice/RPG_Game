using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ������Ʈ Ǯ����
public class MonsterPool : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private int poolSize;

    private Queue<GameObject> activePool = new Queue<GameObject>();
    private Queue<GameObject> disactivePool = new Queue<GameObject>();

    /// <summary>
    /// ���� ����
    /// </summary>
    public GameObject GetMonster()
    {
        if (activePool.Count > 0)
        {
            var monster = disactivePool.Dequeue();
            monster.SetActive(true);
            return monster;
        }
        else
        {
            // Ǯ�� ���Ͱ� ���� ��� ���� ����
            return CreateNewMonster();
        }
    }

    private GameObject CreateNewMonster()
    {
        var newMonster = Instantiate(monsterPrefab);
        newMonster.transform.SetParent(this.transform);
        newMonster.SetActive(true);
        activePool.Enqueue(newMonster);
        return newMonster;
    }

    /// <summary>
    /// ���� ��Ȱ��ȭ
    /// </summary>
    public void ReturnMonster(GameObject monster)
    {
        if (activePool.Contains(monster))
        {
            monster.SetActive(false);
            if (disactivePool.Count > poolSize)
                DestroyImmediate(monster);
            else
                disactivePool.Enqueue(monster);
        }
        else
            Debug.LogError(monster.name + "�̶�� �̸��� ���ʹ� ����Ǯ�� �����ϴ�.");
    }
}
