using UnityEngine;
using System;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game Data/ScoreData")]
public class ScoreData : ScriptableObject
{
    public const int ScorePerItem = 100;
    public static int scorePerRow = 1500;
    public int levelTime = 900;
    public int speedUp = 300;

    public int bestScore = 0;
    public int currentScore;
    
    void OnEnable()
    {
        GameEvents.OnScoreChanged += UpdateScore;
    }
    
    void OnDisable()
    {
        GameEvents.OnScoreChanged -= UpdateScore;
    }
    
    public int GetBestScore()
    {
        return bestScore;
    }
    
    
    private void UpdateScore(int score)
    {
        currentScore = score;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
        }
    }

    public void ResetScoreData()
    {
        currentScore = 0;
    }
}