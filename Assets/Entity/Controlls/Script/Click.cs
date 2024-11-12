using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class Click : MonoBehaviour // Handles clicks.
    {
        private CursorRay ray;

        private void Start()
        {
            ray = new CursorRay();
        }
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
                    obj.GetComponent<UnitMovement>().SetPoint(ray.Ray());
            }
        }
    }
}
