using UnityEngine;

namespace RTS
{
    public class Mine : MonoBehaviour, IHarvest // Gives resources to the harvester
    {
        [SerializeField] private float maxOre, countOre;
        [SerializeField] private GameObject harvestPoint;
        public bool empty { get; private set; }
        public GameObject currentHarvester { get; set; }
        public Vector3 point => harvestPoint.transform.position;

        private void Start()
        {
            countOre = maxOre;
        }

        private void Change(float c) =>
            countOre = Mathf.Clamp(countOre + c, 0, maxOre);

        public void Give(float c)
        {
            if (empty || !currentHarvester) return;
            Change(-c);
            if (countOre == 0) empty = true;
        }

        public void Recovery(float c)
        {
            if (c <= 0) return;
            Change(c);
            empty = false;
        }

        public void SetHarvester(GameObject h) => currentHarvester = h;
    }
}
