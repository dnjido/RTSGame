using UnityEngine;

namespace RTS
{
    public class CameraZoom : MonoBehaviour // Camera zoom
    {
        [SerializeField]private float zoomSpeed = 2f;
        [SerializeField]private float minZoom = 5f;
        [SerializeField]private float maxZoom = 20f;

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
