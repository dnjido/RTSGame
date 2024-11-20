using System;
using UnityEngine;

namespace RTS
{
    public class DetectEnemy : MonoBehaviour //Detection of the enemy within range.
    {
        [field: SerializeField] public float range { get; private set; }
        [SerializeField] private Collider target;
        [SerializeField] private GameObject unit;

        void Update() => Check();

        private void Check()
        {
            Collider[] hitColliders = Physics.OverlapCapsule(transform.position, 
                transform.position, range, 
                DetectLayers.Layers(GetComponent<UnitTeam>()));

            if (hitColliders.Length == 0 && target) { Clear(); return; }

            if (hitColliders.Length == 0) return;

            if (!target) Find(hitColliders);
            else Target(hitColliders);
        }

        private void Find(Collider[] hitColliders)
        {
            foreach (Collider col in hitColliders)
            {
                GameObject u = col.gameObject;
                //if (gameObject != unit && unit.GetComponent<UnitTeam>().team.team != GetComponent<UnitTeam>().team.team){ }
                target = col;
                unit = u;
                break;
            }
        }

        private void Target(Collider[] hitColliders)
        {
            Collider col = Array.Find(hitColliders, c => c == target);
            if (!col) Clear();
        }

        private void Clear()
        {
            target = null; 
            unit = null;
        }

        public GameObject GetUnit() => unit;

        public void SetTarget(GameObject u)
        {
            unit = u;
            target = u.GetComponent<Collider>();
        }
    }

    public class DetectLayers
    {
        public static int Layers(UnitTeam team)
        {
            int l = 11111111 << 7;
            int t = 1 << 6 + team.team.team;
            t = ~t;
            return l & t;
        }
    }
}
