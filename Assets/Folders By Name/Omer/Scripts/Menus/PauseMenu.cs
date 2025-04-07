using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    [SerializeField] private GameObject pauseMenuUI;
    public static event Action OnResumeGame;
    public static event Action OnPauseGame;
    public static event Action OnLoadMenu;
    
    
    // Update is called once per frame
    
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "ShabbatToday")
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (GameIsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    
    public void ResumeGame()
    {
        OnResumeGame?.Invoke();
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }
    
    public void PauseGame()
    {
        OnPauseGame?.Invoke();
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
    }
    
    public void LoadMenu()
    {
        OnLoadMenu?.Invoke();
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        // Load the main menu scene
        
        SceneManager.LoadScene("StartMenu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
