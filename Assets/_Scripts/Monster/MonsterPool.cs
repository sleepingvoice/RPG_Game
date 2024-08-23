using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터 오브젝트 풀링용
public class MonsterPool : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private int poolSize;

    private Queue<GameObject> activePool = new Queue<GameObject>();
    private Queue<GameObject> disactivePool = new Queue<GameObject>();

    /// <summary>
    /// 몬스터 생성
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
            // 풀에 몬스터가 없는 경우 새로 생성
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
    /// 몬스터 비활성화
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
            Debug.LogError(monster.name + "이라는 이름의 몬스터는 몬스터풀에 없습니다.");
    }
}
