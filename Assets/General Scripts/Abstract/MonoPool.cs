using System.Collections.Generic;
using UnityEngine;

public class MonoPool<T> : MonoSingleton<MonoPool<T>> where T : MonoBehaviour, IPoolable
{
    [SerializeField] private int initialPoolSize = 1;
    [SerializeField] private T prefab;
    [SerializeField] private Transform poolParent;
    private Stack<T> _availableObjects;

    private void Awake()
    {
        _availableObjects = new Stack<T>();
        AddItemsToPool();
    }

    private void AddItemsToPool()
    {
        for (var i = 0; i < initialPoolSize; i++)
        {
            var obj = Instantiate(prefab, poolParent, true);
            obj.gameObject.SetActive(false);
            _availableObjects.Push(obj);
        }
    }

    public T Get()
    {
        if (_availableObjects.Count == 0)
            AddItemsToPool();

        var obj = _availableObjects.Pop();
        obj.gameObject.SetActive(true);
        obj.Reset();
        return obj;
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _availableObjects.Push(obj);
    }
}