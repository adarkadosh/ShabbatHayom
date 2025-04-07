using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnEndGame : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    
    public void ActivateGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
}
