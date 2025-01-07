using System;
using Unity.VisualScripting;
using UnityEngine;

namespace RTS
{
    public class FogClear : MonoBehaviour // Ñreate an area that dispels the fog of war.
    {
        [SerializeField] private GameObject FogCleaner;
        [SerializeField] private float yOffset, size;
        private GameObject cleaner;

        private int team => GetComponent<UnitTeam>().team;
        private int layer => GetComponent<UnitTeam>().relationship;
        bool equal => ((1 << 0) & layer) != 0;

        private void Start() => Make();

        public void Make()
        {
            if (!equal) return;

            cleaner = Instantiate(FogCleaner, transform, false);
            cleaner.transform.localPosition = new Vector3(0, yOffset, 0);
            cleaner.transform.localScale = new Vector3(size, size, 1);
        }

        public void Remove()
        {
            if (!cleaner) return;
            Destroy(cleaner);
        }
    }
}
