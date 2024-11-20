using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public struct SelectedUnitsList
    {
        public GameObject[] units;
    }

    public class SelectedUnits // Stores information about the selected units.
    {
        public SelectedUnitsList selected = new SelectedUnitsList();
        private int count;

        public void AddUnits(Collider[] col)
        {
            if (col.Length != count) ClearUnits();
            count = col.Length;

            List<GameObject> u = new List<GameObject>();
            foreach (Collider collider in col)
            {
                Selection sel = collider.gameObject.GetComponent<Selection>();
                //TeamNumber team = collider.gameObject.GetComponent<TeamNumber>();
                if (sel)
                {
                    sel.isSelected = true;
                    u.Add(collider.gameObject);
                }
            }
            selected.units = u.ToArray();
        }

        public void ClearUnits()
        {
            List<GameObject> u = new List<GameObject>();
            selected.units = u.ToArray();
            foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
            {
                Selection sel = unit.GetComponent<Selection>();
                if (sel)
                    sel.isSelected = false;
            }
        }
    }
}
