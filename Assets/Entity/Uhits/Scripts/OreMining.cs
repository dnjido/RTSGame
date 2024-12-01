using UnityEngine;
using System.Collections;

namespace RTS
{
    public class OreMining : MonoBehaviour // Resource harvesting by the harvester
    {
        [SerializeField] private float maxLoad, currentLoad, oreCount, loadDelay;
        private bool canLoad = true;
        public bool empty { get; private set; }
        public bool full { get; private set; }

        private void Change(float c) =>
            currentLoad = Mathf.Clamp(currentLoad + c, 0, maxLoad);

        public void Load(GameObject target)
        {
            if (full || !canLoad) return;

            target.GetComponent<IHarvest>().currentHarvester = gameObject;
            empty = false;
            StartCoroutine(HarvestCoroutine(oreCount));
            if (currentLoad >= maxLoad) full = true;
        }

        public void Unload(GameObject target)
        {
            if (empty || !canLoad) return;

            target.GetComponent<IHarvest>().currentHarvester = gameObject;
            full = false;
            StartCoroutine(HarvestCoroutine(-oreCount));
            if (currentLoad <= 0) empty = true;
        }

        private IEnumerator HarvestCoroutine(float c)
        {
            Change(c);
            canLoad = false;
            yield return new WaitForSeconds(loadDelay);
            canLoad = true;
        }
    }
}
