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
    }
}