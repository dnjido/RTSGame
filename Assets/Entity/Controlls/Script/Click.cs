using UnityEngine;

namespace RTS
{
    public class Click : MonoBehaviour // Handles clicks.
    {
        void Update() 
        {
            if (Input.GetMouseButtonDown(1)) MoveTo();
        }

        public void MoveTo()
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Unit"))
            {
                Selection sel = obj.GetComponent<Selection>();
                if (sel && sel.isSelected)
                    SetTarget(obj);
            }
        }

        public void SetTarget(GameObject obj)
        {
            UnitMovement um = obj.GetComponent<UnitMovement>();
            CursorRay ray = new CursorRay();
            if (ray.RayUnit() != null && ray.RayUnit().tag == "Unit")
                um.SetUnit(ray.RayUnit());
            else
                um.SetPoint(ray.RayPoint());
        }
    }
}
