using UnityEngine;

namespace RTS
{
    public class HideUnit : MonoBehaviour // Hides enemy units in fog of war
    {
        public bool visibile { get; private set; }

        private int team => GetComponent<UnitTeam>().team;

        private void Update()
        {
            Hide();
        }

        private void Hide()
        {
            if (team == 1) return;

            visibile = Ray();

            if (visibile == transform.GetChild(0).gameObject.activeSelf) return;

            foreach (Transform t in transform)
               t.gameObject.SetActive(visibile);
        }

        private bool Ray()
        {
            RaycastHit hitInfo;
            Physics.Raycast(transform.position, Vector3.down * 100, out hitInfo, Mathf.Infinity, 1 << 3);

            return hitInfo.transform != null;

        }
    }
}