using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

            List<GameObject> units = new List<GameObject>();

            if(col.Length <= 0) { }
            else if(HasAttr(col, "Combat").Count > 0) units = HasAttr(col, "Combat");
            else if (HasAttr(col, "Harvester").Count > 0) units = HasAttr(col, "Harvester");
            else units = new List<GameObject> { col[0].gameObject };

            List <GameObject> u = new List<GameObject>();
            
            foreach (GameObject unit in units) 
            {
                Selection sel = unit.GetComponent<Selection>();
                if (CanSelecting(unit))
                {
                    sel.isSelected = true;
                    u.Add(unit);
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

        private bool CanSelecting(GameObject u1) => 
            GU.Team(u1.gameObject) == 1 && u1.GetComponent<Selection>();

        private bool HasCombat(GameObject u)
        {
            return GU.HasAttribute(u, "Combat");
        }

        private List<GameObject> HasAttr(Collider[] array, string tag)
        {
            List<GameObject> list = new List<GameObject>();
            foreach (Collider c in array)
            {
                GameObject u = c.gameObject;
                if (GU.HasAttribute(u, tag)) list.Add(u);
            }
            return list;
        }
    }
}
