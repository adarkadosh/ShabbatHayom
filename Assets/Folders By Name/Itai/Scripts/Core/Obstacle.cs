using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour, IPoolables
{
    private float _speed = 5f;
    private float _timeToSpeedUp = 5f;
    private SpriteRenderer _spriteRenderer;

    private readonly Vector3[] _startPositions =
    {
        new(-3.7f, 13, 0),
        new(0, 10, 0),
        new(3.7f, 13, 0)
    };
    
    [SerializeField] private Sprite[] obstacleSprites;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = obstacleSprites[Random.Range(0, obstacleSprites.Length)];
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
            ObstaclePool.Instance.Return(this);
        }
    }

    public void Reset()
    {
        transform.position = _startPositions[Random.Range(0, _startPositions.Length)];
        _spriteRenderer.sprite = obstacleSprites[Random.Range(0, obstacleSprites.Length)];
        var size = _spriteRenderer.size;
        size.y = Random.Range(10f, 25f);
        _spriteRenderer.size = size;
    }
}