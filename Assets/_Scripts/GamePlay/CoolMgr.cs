using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoolMgr : MonoBehaviour
{
    private SortedDictionary<float, List<UnityAction>> dalayDic = new SortedDictionary<float, List<UnityAction>>();

    public void AddCooldown(UnityAction activeEvent, float delay)
    {
        // ���� �ð��� �����̸� ���ϰ�, �Ҽ� ù° �ڸ����� �ø�
        float triggerTime = Mathf.Ceil((Time.time + delay) * 10) / 10.0f;

        if (!dalayDic.ContainsKey(triggerTime)) // ���� ���� ��ٿ��� ���ٸ�
        {
            dalayDic[triggerTime] = new List<UnityAction>();
        }
        dalayDic[triggerTime].Add(activeEvent);
    }

    private void Update()
    {
        float currentTime = Mathf.Ceil(Time.time * 10) / 10.0f; // ���� �ð��� .1 �������� �ø��Ͽ� ���

        if (dalayDic.Count > 0)
        {
            using (var enumerator = dalayDic.GetEnumerator())
            {
                if (enumerator.MoveNext()) // ù ��° ��ҷ� �̵�
                {
                    var firstDic = enumerator.Current;
                    float nextTriggerTime = firstDic.Key; // ù ��° Ű�� ������

                    if (nextTriggerTime <= currentTime) // ���� ����ð��� ù��° Ű�� �ð����� ũ�ٸ�
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
