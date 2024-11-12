using UnityEngine;

namespace RTS
{
    public class CameraZoom : MonoBehaviour // Camera zoom
    {
        public float zoomSpeed = 2f;
        public float minZoom = 5f;
        public float maxZoom = 20f;

        void Update() => Scroll();

        private bool ClampZoom(Vector3 zoom) =>
            zoom.y > minZoom && zoom.y < maxZoom;

        private void Scroll()
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            if (scrollInput == 0f) return;

            Vector3 newZoom = transform.forward * (scrollInput * zoomSpeed);

            if (ClampZoom(transform.position + newZoom))
                transform.position += newZoom;
        }
    }
}
