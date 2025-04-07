using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private ScoreData scoreData;

    private void Start()
    {
        scoreData.ResetScoreData();
    }

    private void OnEnable()
    {
        GameEvents.GameOver += GameOver;
        GameEvents.RestartGame += RestartGame;
    }

    private void OnDisable()
    {
        GameEvents.GameOver -= GameOver;
        GameEvents.RestartGame -= RestartGame;
    }

    private void GameOver()
    {
        // Handle game over logic here
        SceneManager.LoadScene("GameOverScene");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // when event is triggered, call this restart game method
    public static void RestartGame()
    {
        SceneManager.LoadScene("StartMenu");
        GameEvents.OnGameRestart?.Invoke();
    }
    


    public void ExitGame()
    {
        // Handle game exit logic here
        Debug.Log("Game Over! Exiting...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}