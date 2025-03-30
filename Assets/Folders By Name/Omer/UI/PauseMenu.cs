using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    [SerializeField] private GameObject pauseMenuUI;
    
    // Update is called once per frame
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
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Debug.Log("Game resumed...");
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Debug.Log("Game paused...");
    }
    
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        // Load the main menu scene
        
        // SceneManager.LoadScene("MainMenu"); // TODO: Make the main menu scene
        Debug.Log("Loading main menu...");
        
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
