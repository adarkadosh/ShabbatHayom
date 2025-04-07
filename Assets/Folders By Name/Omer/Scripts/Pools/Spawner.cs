using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Sprite[] _sprites;
    
    private void OnEnable()
    {
        GameEvents.OnProductCollected += SpawnItem;
    }
    private void SpawnItem(Products proudct)
    {
        var item = ItemPool.Instance.Get();
        item.GetComponent<SpriteRenderer>().sprite = _sprites[(int) proudct];
        item.transform.position = _spawnPoint.position;
    }
    
    private void SetSprite(Sprite sprite)
    {
        var item = ItemPool.Instance.Get();
        item.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    
    private void OnDisable()
    {
        GameEvents.OnProductCollected -= SpawnItem;
    }
}
