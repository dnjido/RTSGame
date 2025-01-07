using UnityEngine;
using System.Collections;
using Zenject;

namespace RTS
{
    public class OreMining : MonoBehaviour // Resource harvesting by the harvester
    {
        [SerializeField] private float maxLoad, oreCount, loadDelay;

        [SerializeField] private float currentLoad;

        private bool canLoad = true;

        public bool empty { get; private set; }
        public bool full { get; private set; }

        private void Change(float c) =>
            currentLoad = Mathf.Clamp(currentLoad + c, 0, maxLoad);

        public delegate void HarvestDelegate(float c);
        public event HarvestDelegate HarvestEvent;

        [Inject]
        public void UnitStats(GetStats g)
        {
            maxLoad = g.Stats(gameObject).harvestingStats.maxLoad;
            oreCount = g.Stats(gameObject).harvestingStats.grub;
            loadDelay = g.Stats(gameObject).harvestingStats.delay;
        }

        public void Load(GameObject target)
        {
            if (full || !canLoad) return;
            if (target.GetComponent<Mine>().empty) return;

            SetHarvester(target);
            Ore(oreCount);

            SetFull();
        }

        public void Unload(GameObject target)
        {
            if (empty || !canLoad) return;

            SetHarvester(target);
            Ore(-oreCount);

            SetEmpty();
        }

        public void SetHarvester(GameObject t)
        {
            if (!t.GetComponent<IHarvest>().currentHarvester)
                t.GetComponent<IHarvest>().SetHarvester(gameObject);
        }

        public void Ore(float c)
        {
            HarvestEvent?.Invoke(c);
            StartCoroutine(HarvestCoroutine(c));
        }

        private void SetEmpty()
        {
            full = false;
            if (currentLoad <= 0) empty = true;
        }

        private void SetFull()
        {
            empty = false;
            if (currentLoad >= maxLoad) full = true;
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
