using System;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] private Sprite[] productsPhoneSprites;
    [SerializeField] private SpriteRenderer[] placeHoldersObject;
    
    [Header("Reference to the Product Deque")]
    public ProductDeque productDeque;

    private void OnEnable()
    {
        // GameEvents.OnProductCollected
    }
    
    public void Update()
    {
        // Get the current products from the deque.
        List<Products> products = productDeque.GetProducts();

        for (int i = 0; i < placeHoldersObject.Length; i++)
        {
            if (i < products.Count)
            {
                // Use the product enum as index into productSprites.
                int spriteIndex = (int)products[i];
                if (spriteIndex >= 0 && spriteIndex < productsPhoneSprites.Length)
                {
                    placeHoldersObject[i].sprite = productsPhoneSprites[spriteIndex];
                    placeHoldersObject[i].color = Color.white; // ensure it's visible
                }
            }
            else
            {
                // Clear the placeholder if there is no product.
                placeHoldersObject[i].sprite = null;
                placeHoldersObject[i].color = new Color(1, 1, 1, 0); // make transparent
            }
        }
    }
}
