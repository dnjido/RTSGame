using UnityEngine;

namespace RTS
{
    public class CursorPosition //The position of the cursor on the screen.
    {
        private readonly Camera cam;
        private readonly Canvas can;

        public CursorPosition(Camera camera, Canvas canvas)
        {
            cam = camera;
            can = canvas;
        }

        public Vector2 LocalPos()
        {
            Vector2 loc;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                can.GetComponent<RectTransform>(),
                Input.mousePosition,
                cam,
                out loc);
            return loc;
        }
    }
}