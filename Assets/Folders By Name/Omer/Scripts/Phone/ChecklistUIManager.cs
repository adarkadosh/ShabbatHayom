using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistUIManager : MonoBehaviour
{
    public GameObject checklistItemPrefab;
    public Transform checklistContent;
    public ScrollRect scrollRect;

    public float scrollSpeed = 5f; // Speed for smooth scroll

    private float lastContentHeight;

    public void UpdateChecklist(List<Item> items)
    {
        // Save current height to compare later
        float previousHeight = checklistContent.GetComponent<RectTransform>().rect.height;

        // Clear old items
        foreach (Transform child in checklistContent)
            Destroy(child.gameObject);

        // Add new items
        foreach (Item item in items)
        {
            GameObject newEntry = Instantiate(checklistItemPrefab, checklistContent);
            newEntry.GetComponentInChildren<Image>().sprite = item.itemSprite;

            Text label = newEntry.GetComponentInChildren<Text>();
            if (label != null)
                label.text = item.itemName;
        }

        // Delay scroll to allow layout to update
        StartCoroutine(SmoothScrollIfNeeded(previousHeight));
    }

    private IEnumerator SmoothScrollIfNeeded(float previousHeight)
    {
        // Wait until end of frame so UI updates
        yield return new WaitForEndOfFrame();

        float newHeight = checklistContent.GetComponent<RectTransform>().rect.height;

        // Only scroll if content actually grew
        if (newHeight > previousHeight)
        {
            float target = 0f; // Bottom
            while (Mathf.Abs(scrollRect.verticalNormalizedPosition - target) > 0.001f)
            {
                scrollRect.verticalNormalizedPosition = Mathf.Lerp(
                    scrollRect.verticalNormalizedPosition,
                    target,
                    Time.deltaTime * scrollSpeed
                );
                yield return null;
            }

            scrollRect.verticalNormalizedPosition = target; // Snap to final position
        }
    }
}
