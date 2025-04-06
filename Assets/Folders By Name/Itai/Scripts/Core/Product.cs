using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Product : MonoBehaviour, IPoolable
    { 
        private static float _speed = 5f;
        private SpriteRenderer _spriteRenderer;
        private Products _productType;
        
        [SerializeField] private Sprite[] grocerySprites;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Reset();
        }
        
        private void OnEnable()
        {
            GameEvents.OnSpeedUp += SpeedItUp;
        }
    
        private void OnDisable()
        {
            GameEvents.OnSpeedUp -= SpeedItUp;
        }

        private void SpeedItUp()
        {
            _speed += 0.5f;
        }

        private void FixedUpdate()
        {
            transform.position += Vector3.down * (Time.fixedDeltaTime * _speed);
            if (transform.position.y < -8)
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

