using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Product : MonoBehaviour, IPoolable
    { 
        private SpriteRenderer _spriteRenderer;
        private Products _productType;
        
        [SerializeField] private Sprite[] grocerySprites;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Reset();
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.down * (SpawnManager.Instance.speed * Time.deltaTime));
            if (transform.position.y < -10)
            {
                GroceriesPool.Instance.Return(this);
            }
        }

        public void Reset()
        {
            var myEnumMemberCount = Enum.GetNames(typeof(Products)).Length;
            var randomIndex = Random.Range(0, myEnumMemberCount);
            _productType = (Products) randomIndex;
            _spriteRenderer.sprite = grocerySprites[randomIndex];
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            GameEvents.OnProductCollected.Invoke(_productType);
            GroceriesPool.Instance.Return(this);
        }
    }

