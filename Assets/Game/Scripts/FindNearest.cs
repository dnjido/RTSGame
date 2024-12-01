using UnityEngine;

namespace RTS
{
    public class FindNearest
    {
        public static GameObject FindObject(GameObject[] Objects, Vector3 Point)
        {
            GameObject nearestObject = null;
            float minDistantion = float.MaxValue;

            foreach (GameObject obj in Objects)
            {
                float dist = Vector3.Distance(Point, obj.transform.position);
                if (dist < minDistantion)
                {
                    minDistantion = dist;
                    nearestObject = obj;
                }
            }
            return nearestObject;
        }
    }
}
