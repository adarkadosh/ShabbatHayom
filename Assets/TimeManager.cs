// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class TimeManager : MonoBehaviour
// {
//     public TextMeshPro clockText; // Assign a UI Text element to display the time
//     private float _elapsedTime = 0f;
//     private const float TimeConversionFactor = 900f / 90f; // Equals 10
//
//     public void Update()
//     {
//         // Accumulate elapsed real-world time
//         _elapsedTime += Time.deltaTime;
//
//         // Convert to in-game time
//         float inGameTime = _elapsedTime * TimeConversionFactor;
//
//         // Calculate in-game hours and minutes
//         int hours = Mathf.FloorToInt(inGameTime / 60);
//         int minutes = Mathf.FloorToInt(inGameTime % 60);
//
//         // Update the clock display
//         if (clockText != null)
//         {
//             clockText.text = $"{hours:D2}:{minutes:D2}";
//         }
//     }
// }

using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public TextMeshPro clockText; // Assign a UI TextMeshProUGUI element to display the time
    private float _elapsedTime = 15 * 60; // Start at 15 minutes
    private const float TimeConversionFactor = 900f / 90f; // Equals 10
    private bool _isBlinking = false;

    private void Update()
    {
        // Decrease elapsed real-world time
        _elapsedTime -= Time.deltaTime * TimeConversionFactor;

        // Ensure time doesn't go below zero
        if (_elapsedTime < 0)
        {
            _elapsedTime = 0;
        }

        // Calculate in-game minutes and seconds
        int minutes = Mathf.FloorToInt(_elapsedTime / 60);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60);

        // Update the clock display
        if (clockText != null)
        {
            clockText.text = $"{minutes:D2}:{seconds:D2}";

            // Start blinking when time is 00:30 or less
            if (_elapsedTime <= 300 && !_isBlinking)
            {
                _isBlinking = true;
                StartCoroutine(BlinkText());
            }
        }
    }

    private IEnumerator BlinkText()
    {
        while (_elapsedTime > 0)
        {
            clockText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            clockText.color = Color.black;
            yield return new WaitForSeconds(0.5f);
        }
        clockText.color = Color.white; // Ensure text is white when timer ends
    }
}