using System;
using UnityEngine;

namespace RTS
{
    public class DetectEnemy : MonoBehaviour //Detection of the enemy within range.
    {
        [SerializeField] private float range;
        [SerializeField] private Collider target;
        [SerializeField] private GameObject unit;
        //[SerializeField] private int layer;
        // Start is called before the first frame update
        void Start()
        {
            //if (layer == 0) layer = SetLayers();
        }

        // Update is called once per frame
        void Update() => Check();

        private int SetLayers()
        {
            int l = 11111111 << 7;
            UnitTeam team = GetComponent<UnitTeam>();
            int t = 1 << 6 + team.team.team;
            t = ~t;
            return l & t;
        }

        private void Check()
        {
            //Collider[] hitColliders = new Collider[0];
            //Physics.OverlapCapsuleNonAlloc(transform.position, transform.position, range, hitColliders, 1 << 6);
            Collider[] hitColliders = Physics.OverlapCapsule(transform.position, transform.position, range, SetLayers());

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
    }
}
