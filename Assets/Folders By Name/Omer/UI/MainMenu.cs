using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool _howToPlayActive = false;
    [SerializeField] private GameObject howToPlayUI;
    [SerializeField] private GameObject mainMenuUI;

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void HowToPlay()
    {
        mainMenuUI.SetActive(_howToPlayActive);
        _howToPlayActive = !_howToPlayActive;
        howToPlayUI.SetActive(_howToPlayActive);
    }
}
