using UnityEngine;

namespace RTS
{
    public class CursorRay //The position of the cursor in the world.
    {
        RaycastHit hitInfo;

        public bool Ray() 
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity);
        }

        public Vector3 RayPoint()
        {
            if (Ray())
                return hitInfo.point;
            else
                return Vector3.zero;
        }

        public GameObject RayUnit()
        {
            if (Ray())
                return hitInfo.transform.gameObject;
            else
                return null;
        }
    }
}

