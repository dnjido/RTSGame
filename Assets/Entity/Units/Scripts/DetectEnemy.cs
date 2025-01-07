using System;
using UnityEngine;
using Zenject;

namespace RTS
{
    public class DetectEnemy : MonoBehaviour //Detection of the enemy within range.
    {
        [SerializeField] private float range;
        //[SerializeField]private int canAttackTarget;

        [SerializeField] private UnitTarget[] canAttackTarget = new UnitTarget[1];
        [SerializeField] private int bitwiseTarget;

        private Collider target;
        private bool stopDetect;
        public int layer;

        private GameObject _unit;
        private GameObject unit { get => _unit;
            set {
                _unit = value;
                TargetEvent?.Invoke(value);}
        }

        public delegate void TargetDelegate(GameObject u);
        public event TargetDelegate TargetEvent;

        [Inject]
        public void UnitStats(GetStats g)
        {
            range = g.Stats(gameObject).attackStats.range;

            UnitTarget[] targ = g.Stats(gameObject).attackStats.canAttackTarget;
            if (targ.Length >= 1)
                canAttackTarget = g.Stats(gameObject).attackStats.canAttackTarget;
            BitwiseAttackTarget();
        }

        void Update() => Check();

        private void BitwiseAttackTarget()
        {
            int lenght = canAttackTarget.Length;

            for (int i = 0; i < lenght; i++)
            {
                int t = 1 << (int)canAttackTarget[i];
                bitwiseTarget = bitwiseTarget | t;
            }
        }

        private bool TargetEqual(GameObject u)
        {
            int b = u.GetComponent<UnitFacade>().getBitwiseTarget;
            return (bitwiseTarget & b) != 0;
        }

        public float GetRange() => range;

        private void Check()
        {
            if (stopDetect) return;

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
                if (TargetEqual(u))
                {
                    target = col;
                    unit = u;
                    break;
                }
            }
        }

        public void SetStop(bool s) => stopDetect = s;

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
            int l = 0b11111111;
            int t = tm.relationship;
            t = ~t; 
            t = l & t;
            
            return t << 7;
        }
    }
}
