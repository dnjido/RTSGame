using System;
using UnityEngine;

namespace RTS
{
    public class DetectEnemy : MonoBehaviour //Detection of the enemy within range.
    {
        [field: SerializeField] public float range { get; private set; }
        private Collider target;
        public int layer;

        private GameObject _unit;
        private GameObject unit { get => _unit;
            set {
                _unit = value;
                TargetEvent?.Invoke(value);} 
        }

        public delegate void TargetDelegate(GameObject u);
        public event TargetDelegate TargetEvent;

        void Update() => Check();

        private void Check()
        {
            Collider[] hitColliders = Physics.OverlapCapsule(transform.position, 
                transform.position, range, layer);

            
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
        public static int Layers(UnitTeam tm)
        {
            int l = 11111111 << 7;
            int t = 1 << 6 + tm.team;
            t = ~t;
            return l & t;
        }
    }
}
