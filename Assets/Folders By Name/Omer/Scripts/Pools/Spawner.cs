using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private Transform _spawnPoint;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var item = ItemPool.Instance.Get();
            item.transform.position = _spawnPoint.position;
            
    // Optionally, set the position or other properties of the item here
        }
    }
}
