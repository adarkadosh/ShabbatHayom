using System.Collections;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [Header("Timers")]
    [SerializeField] private float obstacleSpawnInterval = 2f;
    [SerializeField] private float productSpawnInterval = 1f;
    [SerializeField] private float speedIncreaseInterval = 10f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float speedIncreaseAmount = 1f;

    private readonly bool[] _laneOccupied = new bool[3];
    
    private readonly Vector3[] _productStartPositions = {
        new(-3.7f, 10, 0),
        new(0, 10, 0),
        new(3.7f, 10, 0)
    };
    
    private readonly Vector3[] _obstacleStartPositions = {
        new(-3.7f, 15, 0),
        new(0, 15, 0),
        new(3.7f, 15, 0)
    };
    
    private void Start()
    {
        StartCoroutine(ObstacleSpawnRoutine());
        StartCoroutine(ProductSpawnRoutine());
        StartCoroutine(SpeedIncreaseRoutine());
    }

    private IEnumerator ObstacleSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(obstacleSpawnInterval);

            var lane = GetFreeLane();
            if (lane != -1)
            {
                SpawnObstacle(lane);
            }
        }
    }

    private IEnumerator ProductSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(productSpawnInterval);

            var lane = GetFreeLane();
            if (lane != -1)
            {
                SpawnProduct(lane);
            }
        }
    }

    private IEnumerator SpeedIncreaseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);
            GameEvents.OnSpeedUp.Invoke();
            moveSpeed += speedIncreaseAmount;
        }
    }

    private int GetFreeLane()
    {
        // Shuffle lanes randomly
        int[] lanes = { 0, 1, 2 };
        Utils.ShuffleArray(lanes); // A small utility to randomize array order
        foreach (var lane in lanes)
        {
            if (!_laneOccupied[lane])
                return lane;
        }
        return -1; // No free lane
    }

    private void SpawnObstacle(int lane)
    {
        var spawnPos = _obstacleStartPositions[lane];
        var obstacle = ObstaclePool.Instance.Get();
        obstacle.transform.position = spawnPos;
        _laneOccupied[lane] = true;
        StartCoroutine(FreeLaneAfterDelay(lane, 2f)); // 2 seconds until lane is free again
    }

    private void SpawnProduct(int lane)
    {
        var spawnPos = _productStartPositions[lane];
        var product = GroceriesPool.Instance.Get();
        product.transform.position = spawnPos;
        product.gameObject.SetActive(true);
    }

    private IEnumerator FreeLaneAfterDelay(int lane, float delay)
    {
        yield return new WaitForSeconds(delay);
        _laneOccupied[lane] = false;
    }
}

public static class Utils
{
    public static void ShuffleArray<T>(T[] array)
    {
        for (var i = array.Length - 1; i > 0; i--)
        {
            var rnd = Random.Range(0, i + 1);
            (array[i], array[rnd]) = (array[rnd], array[i]);
        }
    }
}
