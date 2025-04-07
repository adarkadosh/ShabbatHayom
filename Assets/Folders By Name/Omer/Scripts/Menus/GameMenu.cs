using System;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    [SerializeField] private Image[] heartImages;
    [SerializeField] private int maxTries = 2;
    private int _currentTries;

    private void OnEnable()
    {
        GameEvents.OnObstacleHit += AddTry;
    }

    void Start()
    {
        _currentTries = 0;
        UpdateHearts();
    }
    
    public void AddTry()
    {
        if (_currentTries >= maxTries)
        {
            GameEvents.GameOver?.Invoke();
            return;
        }
        _currentTries++;
        UpdateHearts();
    }
    

    private void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i >= _currentTries;
        }
    }
    
    private void OnDisable()
    {
        GameEvents.OnObstacleHit -= AddTry;
    }
}