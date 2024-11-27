using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public class CursorOnUI
    {
        public static bool CursorOverUI()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            return results.Count > 0;
        }
    }
}

