using UnityEngine;
using Zenject;

namespace RTS
{
    public class SetMapCoord : MonoBehaviour
    {
        [SerializeField] private Camera UICamera, MapCamera;
        [SerializeField] private RectTransform rect;
        private float xOffset;
        private bool onUI => CursorOnUI.CursorOverName(gameObject.name);

        private float fov => MapCamera.orthographicSize;
        private Vector2 pos => CursorPosition.LocalPos(UICamera, rect);
        private Vector2 halfSize => rect.sizeDelta / 2;
        private float height => Camera.main.transform.position.y;

        private MoveCommand moveTo;

        [Inject]
        public void Init(MoveCommand m) =>
            moveTo = m;

        void Start() =>
            xOffset = Camera.main.transform.position.x;

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && onUI) Move();
            if (Input.GetMouseButtonDown(1) && onUI) Commnad();
        }

        private Vector3 MakeVector()
        {
            float lerpX = pos.x / halfSize.x;
            float lerpY = pos.y / halfSize.y;

            return new Vector3(lerpY * fov + xOffset, height, -lerpX * fov);
        }

        private void Move() =>
            Camera.main.transform.position = MakeVector();

        private void Commnad() =>
            moveTo.MoveTo();
    }
}
