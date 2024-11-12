using UnityEngine;

namespace RTS
{
    public class CursorRay //The position of the cursor in the world.
    {
        public Vector3 Ray()
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity))
                return hitInfo.point;
            else
                return Vector3.zero;
        }
    }
}

