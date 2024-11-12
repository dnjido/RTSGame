using UnityEngine;

namespace RTS
{
    public class CameraMover : MonoBehaviour // Camera movement
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float edgeThickness = 10f;

        void Update() => Move();

        private void Move()
        {
            Vector3 mov = Vector3.zero;
            Vector3 pos = Input.mousePosition;

            if (pos.x <= edgeThickness)
                mov.z = 1;
            else if (pos.x >= Screen.width - edgeThickness)
                mov.z = -1;
            if (pos.y <= edgeThickness)
                mov.x = -1;
            else if (pos.y >= Screen.height - edgeThickness)
                mov.x = 1;

            transform.position += mov * moveSpeed * Time.deltaTime;
        }
    }
}