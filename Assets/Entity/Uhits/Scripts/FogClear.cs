using UnityEngine;

namespace RTS
{
    public class FogClear : MonoBehaviour // �reate an area that dispels the fog of war.
    {
        [SerializeField] private GameObject FogCleaner;
        [SerializeField] private float yOffset, size;
        private int team => GetComponent<UnitTeam>().team;

        private void Start()
        {
            if (!GetComponent<Placing>()) Make();
        }

        public void Make()
        {
            if (team != 1) return;

            GameObject cleaner = Instantiate(FogCleaner, transform, false);
            cleaner.transform.localPosition = new Vector3(0, yOffset, 0);
            cleaner.transform.localScale = new Vector3(size, size, 1);
        }
    }
}
