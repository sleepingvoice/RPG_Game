using System;
using UnityEngine;

public class BeventHandler
{
    public event Action OnEvent
    {
        add
        {
            _onEvent += value;
        }
        remove
        {
            _onEvent -= value;
        }
    }

    private Action _onEvent;

    private int totalHandlers = 0; // ��ü �ڵ鷯 ��
    private int completedHandlers = 0; // �Ϸ�� �ڵ鷯 ��

    public Action CompletedEvent; // �ݹ� �Լ�

    public void InvokeEvent()
    {
        if (_onEvent == null)
            return;

        completedHandlers = 0;

        totalHandlers = _onEvent.GetInvocationList().Length;

        _onEvent.Invoke();
    }

    public void FinishEvent()
    {
        completedHandlers++;

        if (completedHandlers >= totalHandlers)
        {
            AllEventCompleted();
        }
    }

    private void AllEventCompleted()
    {
        CompletedEvent?.Invoke();
    }

}
