using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoolMgr : MonoBehaviour
{
    private SortedDictionary<float, List<UnityAction>> dalayDic = new SortedDictionary<float, List<UnityAction>>();

    public void AddCooldown(UnityAction activeEvent, float delay)
    {
        // 현재 시간에 딜레이를 더하고, 소수 첫째 자리까지 올림
        float triggerTime = Mathf.Ceil((Time.time + delay) * 10) / 10.0f;

        if (!dalayDic.ContainsKey(triggerTime)) // 만약 같은 쿨다운이 없다면
        {
            dalayDic[triggerTime] = new List<UnityAction>();
        }
        dalayDic[triggerTime].Add(activeEvent);
    }

    private void Update()
    {
        float currentTime = Mathf.Ceil(Time.time * 10) / 10.0f; // 현재 시간을 .1 단위까지 올림하여 사용

        if (dalayDic.Count > 0)
        {
            using (var enumerator = dalayDic.GetEnumerator())
            {
                if (enumerator.MoveNext()) // 첫 번째 요소로 이동
                {
                    var firstDic = enumerator.Current;
                    float nextTriggerTime = firstDic.Key; // 첫 번째 키를 가져옴

                    if (nextTriggerTime <= currentTime) // 만약 현재시간이 첫번째 키의 시간보다 크다면
                    {
                        List<UnityAction> objectsToActivate = firstDic.Value;
                        foreach (var obj in objectsToActivate)
                        {
                            ActivateObject(obj);
                        }
                        dalayDic.Remove(nextTriggerTime);
                    }
                }
            }
        }
    }

    void ActivateObject(UnityAction activeEvent)
    {
        activeEvent.Invoke();
    }
}
