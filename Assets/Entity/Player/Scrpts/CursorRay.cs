using UnityEngine;

namespace RTS
{
    public class CursorRay //The position of the cursor in the world.
    {
        public static RaycastHit RayHit()  
        {
            RaycastHit hitInfo;
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity);
            return hitInfo;
        }

        public static Vector3 RayPoint()
        {
            RaycastHit hit = RayHit();
            if(hit.point != null)
                return hit.point;
            else 
                return Vector3.zero;
        }

        public static GameObject RayUnit()
        {
            RaycastHit hit = RayHit();
            if (hit.transform != null)
                return hit.transform.gameObject;
            else
                return null;
        }
    }
}

