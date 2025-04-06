using System;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private static readonly int Scanned = Animator.StringToHash("Scanned");

    [SerializeField] private Animator _animator;
    
    private void OnEnable()
    {
        PoolableItem.ItemScanned += OnItemScanned;
    }
    
    private void OnItemScanned()
    {
        _animator.SetTrigger(Scanned);
    }
    
    private void OnDisable()
    {
        PoolableItem.ItemScanned -= OnItemScanned;
    }
}
