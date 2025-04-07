using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance
    [SerializeField] private TextMeshPro scoreText; // Reference to the UI text element
    [SerializeField] private TextMeshPro bestScoreText; 
    [SerializeField] private ScoreData scoreData;
    public int currentScore;

    private void Awake()
    {
        bestScoreText.text = "HIGH-SCORE: " + scoreData.GetBestScore().ToString("D10");
        // Implement the singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    // Method to retrieve the current score
    public int GetScore()
    {
        return currentScore;
    }

    // Method to reset the score
    public void ResetScore()
    {
        currentScore = 0;
        Debug.Log("Score reset");
    }
    
    void OnEnable()
    {
        GameEvents.OnScoreChanged += UpdateScore;
        PoolableItem.ItemScanned += OnItemScanned;
        GameEvents.OnRowCleared += OnRowCleared;
        
    }
    
    void OnDisable()
    {
        GameEvents.OnScoreChanged -= UpdateScore;
        PoolableItem.ItemScanned -= OnItemScanned;
        GameEvents.OnRowCleared -= OnRowCleared;
    }
    
    
    private void OnRowCleared()
    {
        currentScore += ScoreData.scorePerRow;
        GameEvents.OnScoreChanged?.Invoke(currentScore);
    }

    // Method to update the score when an item is scanned
    private void OnItemScanned()
    {
        currentScore += ScoreData.ScorePerItem;
        GameEvents.OnScoreChanged?.Invoke(currentScore);
    }
    
    // Method to update the score and UI
    private void UpdateScore(int score)
    {
        if (currentScore >= scoreData.GetBestScore())
        {
            bestScoreText.text = "HIGH-SCORE: " + currentScore.ToString("D10");
        }
        currentScore = score;
        scoreText.text = score.ToString("D10");
    }
}