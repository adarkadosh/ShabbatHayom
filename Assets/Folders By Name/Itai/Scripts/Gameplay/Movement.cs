using UnityEngine;

public class Movement : MonoBehaviour
{
    private static readonly int Hit = Animator.StringToHash("Hit");
    private int _laneNumber = 1;
    private bool _didGoLeft;
    private bool _didGoRight;
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_laneNumber > 0)
            {
                _laneNumber--;
                transform.position += Vector3.left * 3.7f;
                _didGoLeft = true;
                _didGoRight = false;
            }
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (_laneNumber >= 2) return;
            _laneNumber++;
            transform.position += Vector3.right * 3.7f;
            _didGoRight = true;
            _didGoLeft = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameEvents.OnObstacleHit?.Invoke();
            _animator.SetTrigger(Hit);
            if (_didGoLeft)
            {
                _laneNumber++;
                transform.position += Vector3.right * 3.7f;
                _didGoLeft = false;
            }
            else if (_didGoRight)
            {
                _laneNumber--;
                transform.position += Vector3.left * 3.7f;
                _didGoRight = false;
            }
            else
            {
                if (_laneNumber == 0)
                {
                    _laneNumber++;
                    transform.position += Vector3.right * 3.7f;
                }
                else if (_laneNumber == 2)
                {
                    _laneNumber--;
                    transform.position += Vector3.left * 3.7f;
                }
                else
                {
                    _laneNumber = Random.Range(0, 2) == 0 ? 0 : 2;
                    transform.position += _laneNumber == 0 ? Vector3.right * 3.7f : Vector3.left * 3.7f;
                }
            }
        }
    }
}