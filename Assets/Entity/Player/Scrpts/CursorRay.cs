using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RTS
{
    public class CursorRay //The position of the cursor in the world.
    {
        public static Vector3 LandPoint()
        {
            RaycastHit hitInfo;
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 11 >> 0);
            return hitInfo.point;
        }

        public static RaycastHit RayHit(int layer)  
        {
            RaycastHit hitInfo;
            Vector3 point1 = Camera.main.transform.position;
            Vector3 point2 = LandPoint();
            Ray rayOrigin = new Ray(point1, point2 - point1);

            Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, layer);
            return hitInfo;
        }

        public static Vector3 RayPoint()
        {
            RaycastHit hit = RayHit(0b11 >> 0);
            if(hit.point != null)
                return hit.point;
            else 
                return Vector3.zero;
        }

        public static GameObject RayUnit()
        {
            RaycastHit hit = RayHit(0b11111111 << 7);
            if (hit.transform != null)
                return hit.transform.gameObject;
            else
                return null;
        }
    }

    public class CursorOnUI
    {
        public static List<RaycastResult> RaycastResult()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            return results;
        }

        public static bool CursorOverUI()
        {
            return RaycastResult().Count > 0;
        }

        public static bool CursorOverName(string name)
        {
            foreach (var item in RaycastResult())
            {
                if (item.gameObject.name == name) return true;
            }
            return false;
        }
    }
}

