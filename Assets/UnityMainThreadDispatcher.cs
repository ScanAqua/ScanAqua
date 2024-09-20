using System;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static readonly Queue<Action> _executionQueue = new Queue<Action>();
    private static UnityMainThreadDispatcher _instance = null;

    public static UnityMainThreadDispatcher Instance()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<UnityMainThreadDispatcher>();
            if (!_instance)
            {
                Debug.LogError("UnityMainThreadDispatcher�� ���� �����ϴ�.");
            }
        }
        return _instance;
    }

    void Update()
    {
        lock (_executionQueue)
        {
            while (_executionQueue.Count > 0)
            {
                Debug.Log("ť���� �۾��� �����ϴ�.");  // ť���� �۾��� ���������� Ȯ��
                _executionQueue.Dequeue().Invoke();
            }
        }
    }

    public void Enqueue(Action action)
    {
        lock (_executionQueue)
        {
            Debug.Log("�۾��� ť�� �߰��մϴ�.");  // �۾��� ť�� �߰��Ǵ��� Ȯ��
            _executionQueue.Enqueue(action);
        }
    }
}