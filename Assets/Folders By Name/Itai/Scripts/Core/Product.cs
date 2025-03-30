using Folders_By_Name.Itai.Scripts.Abstract;
using UnityEngine;

namespace Folders_By_Name.Itai.Scripts.BoardComponents
{
    public class Product : MonoBehaviour, IPoolable
    { 
        private float _speed = 5f;
        private float _timeToSpeedUp = 5f;
        
        private readonly Vector3[] _startPositions = {
            new(-4, 10, 0),
            new(0, 10, 0),
            new(4, 10, 0)
        };

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
        }
    }
}
