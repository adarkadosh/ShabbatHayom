using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour, IPoolable
{
    private float _speed = 5f;
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private Sprite[] obstacleSprites;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = obstacleSprites[Random.Range(0, obstacleSprites.Length)];
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
        if (transform.position.y < -15)
        {
            ObstaclePool.Instance.Return(this);
        }
    }

    public void Reset()
    {
        _spriteRenderer.sprite = obstacleSprites[Random.Range(0, obstacleSprites.Length)];
        var size = _spriteRenderer.size;
        size.y = Random.Range(10f, 25f);
        _spriteRenderer.size = size;
    }
}