using System.Linq;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;
    private float _backgroundHeight;
    
    private void Start()
    {
        if (backgrounds.Length == 0)
        {
            Debug.LogError("No backgrounds assigned to the BackgroundScroller.");
            return;
        }

        _backgroundHeight = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.y;
        for (var i = 1; i < backgrounds.Length; i++)
        {
            var bgHeight = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.y;
            if (!(Mathf.Abs(_backgroundHeight - bgHeight) > Mathf.Epsilon)) continue;
            Debug.LogError("Backgrounds must have the same height.");
            return;
        }
    }

    private void FixedUpdate()
    {
        foreach (var bg in backgrounds)
        {
            bg.Translate(Vector3.down * (SpawnManager.Instance.speed * Time.deltaTime));

            if (!(bg.position.y <= -_backgroundHeight)) continue;
            var highestY = GetHighestBackgroundY();
            bg.position = new Vector3(bg.position.x, highestY + _backgroundHeight - 1, bg.position.z);
        }
    }

    private float GetHighestBackgroundY()
    {
        return backgrounds.Select(bg => bg.position.y).Prepend(float.MinValue).Max();
    }
}