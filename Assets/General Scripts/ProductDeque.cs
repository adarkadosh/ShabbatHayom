using UnityEngine;
using System;
using System.Collections.Generic;

public class ProductDeque : MonoBehaviour
{
    private readonly LinkedList<Products> _deque = new();

    private void OnEnable()
    {
        GameEvents.OnProductCollected += PushBack;
    }
    
    private void OnDisable()
    {
        GameEvents.OnProductCollected -= PushBack;
    }

    public void AddFront(Products productType)
    {
        _deque.AddFirst(productType);
    }
    
    public void PushBack(Products productType)
    {
        _deque.AddLast(productType);
        print(_deque.First);
    }
    
    public Products PopFront()
    {
        if (_deque.Count == 0)
            throw new InvalidOperationException("Deque is empty");
        
        var product = _deque.First.Value;
        _deque.RemoveFirst();
        return product;
    }
    
    public Products RemoveBack()
    {
        if (_deque.Count == 0)
            throw new InvalidOperationException("Deque is empty");
        
        var product = _deque.Last.Value;
        _deque.RemoveLast();
        return product;
    }
    
    public Products PeekFront()
    {
        if (_deque.Count == 0)
            throw new InvalidOperationException("Deque is empty");
        
        return _deque.First.Value;
    }
    
    public Products PeekBack()
    {
        if (_deque.Count == 0)
            throw new InvalidOperationException("Deque is empty");
        
        return _deque.Last.Value;
    }
    
    public int Count => _deque.Count;
    
    public bool IsEmpty => _deque.Count == 0;
    
    public void Clear()
    {
        _deque.Clear();
    }
    
    public void PrintDeque()
    {
        foreach (var item in _deque)
        {
            Debug.Log(item);
        }
    }
    
    public void PrintDequeReverse()
    {
        for (var node = _deque.Last; node != null; node = node.Previous)
        {
            Debug.Log(node.Value);
        }
    }
    
    public List<Products> GetProducts()
    {
        List<Products> products = new List<Products>();
        foreach (Products product in _deque)
        {
            products.Add(product);
        }
        return products;
    }
}
