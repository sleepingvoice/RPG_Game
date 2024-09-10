using System.Collections.Generic;
using UnityEngine;

//몬스터 오브젝트 풀링용
public class MonsterPool : MonoBehaviour
{
    [SerializeField] private MonsterBase monsterPrefab;
    [SerializeField] private int poolSize;

    private Queue<MonsterBase> activePool = new Queue<MonsterBase>();
    private Queue<MonsterBase> disactivePool = new Queue<MonsterBase>();

    /// <summary>
    /// 몬스터 생성
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
            // 풀에 몬스터가 없는 경우 새로 생성
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
    /// 몬스터 비활성화
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
            Debug.LogError(monster.name + "이라는 이름의 몬스터는 몬스터풀에 없습니다.");
    }
}
