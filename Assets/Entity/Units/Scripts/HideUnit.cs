using UnityEngine;

namespace RTS
{
    public class HideUnit : MonoBehaviour // Hides enemy units in fog of war
    {
        public bool visibile { get; private set; }

        private int team => GetComponent<UnitTeam>().team;
        private int layer => GetComponent<UnitTeam>().relationship;
        bool init = false;
        bool equal => ((1 << 0) & layer) != 0;

        private void Update() => Hide();

        private void Start()
        {
            if (!equal) StartClear(true);
        }

        public void StartClear(bool b) 
        {
            if (equal) init = b;
        } 

        private void Hide()
        {
            if (equal && !init) return;
            visibile = Ray();
            if (visibile == transform.GetChild(0).gameObject.activeSelf) return;
            SetVisibility(visibile);
        }

        private void SetVisibility(bool v)
        {
            foreach (Transform t in transform)
                t.gameObject.SetActive(v);

        }

        private bool Ray()
        {
            RaycastHit hitInfo;
            Physics.Raycast(transform.position, Vector3.down * 100, out hitInfo, Mathf.Infinity, 1 << 3);

            return hitInfo.transform != null;

        }
    }
}