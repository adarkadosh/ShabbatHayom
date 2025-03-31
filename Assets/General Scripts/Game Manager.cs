using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    private void OnEnable()
    {
        GameEvents.OnObstacleHit += ExitGame;
    }

    private void OnDisable()
    {
        GameEvents.OnObstacleHit -= ExitGame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // when event is triggered, call this restart game method
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void Start()
    {
        // Initialize game state or other components here
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