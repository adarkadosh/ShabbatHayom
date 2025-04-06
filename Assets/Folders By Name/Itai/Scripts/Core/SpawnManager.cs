using System.Collections;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [Header("Lanes")]
    public float[] laneXPositions = { -2f, 0f, 2f }; // X positions for 3 lanes
    
    [Header("Timers")]
    public float obstacleSpawnInterval = 2f;
    public float productSpawnInterval = 1f;
    public float speedIncreaseInterval = 10f;
    [SerializeField] private float spawnIntervalVariation = 0.5f;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float speedIncreaseAmount = 1f;

    private bool[] laneOccupied = new bool[3];

    private float _nextSpawnTime;

    private void Start()
    {
        StartCoroutine(ObstacleSpawnRoutine());
        StartCoroutine(ProductSpawnRoutine());
        StartCoroutine(SpeedIncreaseRoutine());
    }
    
    IEnumerator ObstacleSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(obstacleSpawnInterval);

            int lane = GetFreeLane();
            if (lane != -1)
            {
                SpawnObstacle(lane);
            }
        }
    }

    IEnumerator ProductSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(productSpawnInterval);

            int lane = GetFreeLane();
            if (lane != -1)
            {
                SpawnProduct(lane);
            }
        }
    }

    IEnumerator SpeedIncreaseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);
            moveSpeed += speedIncreaseAmount;
        }
    }
    
    int GetFreeLane()
    {
        // Shuffle lanes randomly
        int[] lanes = { 0, 1, 2 };
        Utils.ShuffleArray(lanes); // A small utility to randomize array order

        foreach (int lane in lanes)
        {
            if (!laneOccupied[lane])
                return lane;
        }

        return -1; // No free lane
    }

    void SpawnObstacle(int lane)
    {
        Vector3 spawnPos = new Vector3(laneXPositions[lane], transform.position.y, 0f);
        Obstacle obstacle = ObstaclePool.Instance.Get();
        obstacle.transform.position = spawnPos;
        obstacle.SetActive(true);
        laneOccupied[lane] = true;
        StartCoroutine(FreeLaneAfterDelay(lane, 2f)); // 2 seconds until lane is free again
    }

    void SpawnProduct(int lane)
    {
        Vector3 spawnPos = new Vector3(laneXPositions[lane], transform.position.y, 0f);
        GameObject product = ObjectPooler.Instance.GetFromPool("Product");
        product.transform.position = spawnPos;
        product.SetActive(true);
    }

    IEnumerator FreeLaneAfterDelay(int lane, float delay)
    {
        yield return new WaitForSeconds(delay);
        laneOccupied[lane] = false;
    }

    private void Update()
    {
        if (!(Time.time >= _nextSpawnTime)) return;
        Spawn();
        _nextSpawnTime = Time.time + spawnInterval +
                         Random.Range(-spawnIntervalVariation, spawnIntervalVariation);
    }

    private void Spawn()
    {
        if (Random.Range(0, 2) == 0)
        {
            var product = GroceriesPool.Instance.Get();
        }
        else
        {
            var obstacle = ObstaclePool.Instance.Get();
        }
    }
}