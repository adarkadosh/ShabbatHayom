using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MonoPool<PoolableItem> itemPool;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var item = Itewne.Instance.Get();
            // Optionally, set the position or other properties of the item here
        }
    }
}
