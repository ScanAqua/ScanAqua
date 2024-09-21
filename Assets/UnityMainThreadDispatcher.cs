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
                Debug.LogError("UnityMainThreadDispatcher가 씬에 없습니다.");
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
                Debug.Log("큐에서 작업을 꺼냅니다.");  // 큐에서 작업이 꺼내지는지 확인
                _executionQueue.Dequeue().Invoke();
            }
        }
    }

    public void Enqueue(Action action)
    {
        lock (_executionQueue)
        {
            Debug.Log("작업을 큐에 추가합니다.");  // 작업이 큐에 추가되는지 확인
            _executionQueue.Enqueue(action);
        }
    }
}