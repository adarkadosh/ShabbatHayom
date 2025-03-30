using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public float speed = 5f;
    private bool shouldMove = false;

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag($"Board"))
        {
            shouldMove = true;
        }
    }
}
