using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class AIAttack // Make AI attack  wave to enemy
    {
        readonly private AIManadger manadger;

        private int team;

        private bool RelationshipEqual(GameObject u) => 
            (GU.Relationship(u) & (1 << team)) != 0;

        public AIAttack(AIManadger m)
        {
            manadger = m;
            team = m.getTeam;
            manadger.AIAttackEvent += GetCombatUnits;
        }

        private void GetCombatUnits()
        {
            List<GameObject> units = GetTeam(GetUnits("Combat").ToArray(), true);
            Command(units);
        }

        private List<GameObject> GetUnits(string attr)
        {
            List<GameObject> unit = new List<GameObject>(); 
            GameObject[] tag = GameObject.FindGameObjectsWithTag("Unit");

            foreach (GameObject u in tag)
                if (GetAttribute(u, attr))
                    unit.Add(u);

            return unit;
        }

        private bool GetAttribute(GameObject unit, string attr)
        {
            foreach (string a in GU.Attribute(unit))
                if (a == attr) return true;

            return false;
        }

        private List<GameObject> GetTeam(GameObject[] unit, bool ally)
        {
            List<GameObject> list = new List<GameObject>();

            foreach (GameObject u in unit)
                if (RelationshipEqual(u) == ally) list.Add(u);

            return list;
        }

        private void Command(List<GameObject> list)
        {
            foreach (GameObject unit in list)
                unit.GetComponent<UnitMovement>().MoveAI(GetPoint());
        }

        private Vector3 GetPoint()
        {
            List<GameObject> yards = GetTeam(GetUnits("Yard").ToArray(), false);
            return yards[Random.Range(0, yards.Count)].transform.position;
        }
    }
}
