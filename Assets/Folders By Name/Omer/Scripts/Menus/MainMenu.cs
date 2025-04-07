using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool _howToPlayActive = false;
    [SerializeField] private GameObject howToPlayUI;
    [SerializeField] private GameObject mainMenuUI;
    
    public static event Action OnLoadGame;
    public void LoadGame()
    {
        OnLoadGame?.Invoke();
        SceneManager.LoadScene($"ShabbatToday");
    }

    public void HowToPlay()
    {
        mainMenuUI.SetActive(_howToPlayActive);
        _howToPlayActive = !_howToPlayActive;
        howToPlayUI.SetActive(_howToPlayActive);
    }
    
    public void CloseExpleanation()
    {
        mainMenuUI.SetActive(true);
        _howToPlayActive = false;
        howToPlayUI.SetActive(false);
    }
}
