using System;
using UnityEngine;

public class PoolableItem : MonoBehaviour, IPoolable
{
    public float speed = 5f;
    private bool _shouldMove = false;
    public void Reset()
    {
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldMove)
        {
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag($"Board"))
        {
            _shouldMove = true;
        }
        
        else if (other.CompareTag($"Destroyer"))
        {
            _shouldMove = false;
            ItemPool.Instance.Return(this);
        }
    }
}
