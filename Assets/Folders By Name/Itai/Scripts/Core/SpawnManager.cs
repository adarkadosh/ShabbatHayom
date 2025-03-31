using Folders_By_Name.Itai.Scripts.Abstract;
using UnityEngine;

namespace Folders_By_Name.Itai.Scripts.BoardComponents
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        [SerializeField] private Sprite[] grocerySprites;
        [SerializeField] private Sprite[] obstacleSprites;
        [SerializeField] private float spawnInterval = 1f;
        [SerializeField] private float spawnIntervalVariation = 0.5f;
        
        private float _nextSpawnTime;
        
        private void Start()
        {
            _nextSpawnTime = Time.time + spawnInterval;
        }
        
        private void Update()
        {
            if (!(Time.time >= _nextSpawnTime)) return;
            Spawn();
            _nextSpawnTime = Time.time + spawnInterval + Random.Range(-spawnIntervalVariation, spawnIntervalVariation);
        }
        
        private void Spawn()
        {
            if (Random.Range(0, 2) == 0)
            {
                var product = GroceriesPool.Instance.Get();
                // product.GetComponent<SpriteRenderer>().sprite = grocerySprites[Random.Range(0, grocerySprites.Length)];
            }
            else
            {
                var obstacle = ObstaclePool.Instance.Get();
                // obstacle.GetComponent<SpriteRenderer>().sprite = obstacleSprites[Random.Range(0, obstacleSprites.Length)];
            }
        }
    }
}
