using UnityEngine;

namespace RTS
{
    public class CursorPosition //The position of the cursor on the screen.
    {
        public static Vector2 LocalPos(Camera cam, Canvas can)
        {
            Vector2 loc;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                can.GetComponent<RectTransform>(),
                Input.mousePosition,
                cam,
                out loc);
            return loc;
        }

        public static Vector2 LocalPos(Camera cam, RectTransform rect)
        {
            Vector2 loc;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rect,
                Input.mousePosition,
                cam,
                out loc);
            return loc;
        }

        public static Vector2 Pos(Camera cam, Canvas can, Vector3 pos)
        {
            Vector2 loc;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                can.GetComponent<RectTransform>(),
                pos,
                cam,
                out loc);
            return loc;
        }
    }
}