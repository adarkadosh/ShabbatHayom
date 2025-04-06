using System;
using General_Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class Product : MonoBehaviour, IPoolable
    { 
        private float _speed = 5f;
        private float _timeToSpeedUp = 5f;
        private SpriteRenderer _spriteRenderer;
        private Products _productType;
        
        private readonly Vector3[] _startPositions = {
            new(-3.7f, 10, 0),
            new(0, 10, 0),
            new(3.7f, 10, 0)
        };
        
        [SerializeField] private Sprite[] grocerySprites;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = grocerySprites[Random.Range(0, grocerySprites.Length)];
        }

        private void FixedUpdate()
        {
            transform.position += Vector3.down * (Time.fixedDeltaTime * _speed);
            if (Time.time >= _timeToSpeedUp)
            {
                _speed += 0.5f;
                _timeToSpeedUp += 5f;
            }
            if (transform.position.y < -8)
            {
                GroceriesPool.Instance.Return(this);
            }
        }

        public void Reset()
        {
            transform.position = _startPositions[Random.Range(0, _startPositions.Length)];
            var myEnumMemberCount = Enum.GetNames(typeof(Products)).Length;
            var randomIndex = Random.Range(0, myEnumMemberCount);
            _productType = (Products) randomIndex;
            _spriteRenderer.sprite = grocerySprites[randomIndex];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameEvents.OnProductCollected.Invoke(_productType);
            GroceriesPool.Instance.Return(this);
        }
    }

