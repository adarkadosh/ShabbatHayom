using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour, IPoolable
{ 
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private Sprite[] obstacleSprites;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = obstacleSprites[Random.Range(0, obstacleSprites.Length)];
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * (SpawnManager.Instance.speed * Time.deltaTime));
        if (transform.position.y < -15)
        {
            ObstaclePool.Instance.Return(this);
        }
    }


    public void Reset()
    {
        _spriteRenderer.sprite = obstacleSprites[Random.Range(0, obstacleSprites.Length)];
        var size = _spriteRenderer.size;
        size.y = Random.Range(8f, 15f);
        _spriteRenderer.size = size;
    }
}