using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    struct SelectedUnitsList
    {
        public GameObject[] units;
    }

    public class SelectedUnits // Stores information about the selected units.
    {
        SelectedUnitsList selected = new SelectedUnitsList();

        public void AddUnits(Collider[] col)
        {
            List<GameObject> u = new List<GameObject>();
            ClearUnits();
            foreach (Collider collider in col)
            {
                Selection sel = collider.gameObject.GetComponent<Selection>();
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
