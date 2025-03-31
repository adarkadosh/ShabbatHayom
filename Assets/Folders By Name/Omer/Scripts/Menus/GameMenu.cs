using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    [SerializeField] private Image[] heartImages;
    [SerializeField] private int maxTries = 3;
    private int _currentTries;

    void Start()
    {
        _currentTries = 0;
        UpdateHearts();
    }

    void Update()
    {
        // For testing purposes, increase tries when pressing the T key
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddTry();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTries();
        }
    }

    public void AddTry()
    {
        _currentTries++;
        if (_currentTries > maxTries) _currentTries = maxTries;
        UpdateHearts();
    }

    public void ResetTries()
    {
        _currentTries = 0;
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i >= _currentTries;
        }
    }
}