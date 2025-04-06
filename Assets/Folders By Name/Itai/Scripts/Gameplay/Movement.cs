using UnityEngine;

public class Movement : MonoBehaviour
{
    private int _laneNumber = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_laneNumber > 0)
            {
                _laneNumber--;
                transform.position += Vector3.left * 4;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_laneNumber >= 2) return;
            _laneNumber++;
            transform.position += Vector3.right * 4;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameEvents.OnObstacleHit?.Invoke();
        }
        else if (other.CompareTag("Product"))
        {
            // MyEvents.OnProductCollected?.Invoke(other.GetComponent<SpriteRenderer>().sprite);
            other.gameObject.SetActive(false);
        }
    }
}