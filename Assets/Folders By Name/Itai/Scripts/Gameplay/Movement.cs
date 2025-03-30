using UnityEngine;

namespace Folders_By_Name.Itai.Scripts.Cart
{
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
    }
}
