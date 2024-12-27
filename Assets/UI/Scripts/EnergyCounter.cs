using UnityEngine;
using Zenject;

namespace RTS
{
    public class EnergyCounter : Counters // Displays amount of Energy
    {
        protected override float resource => currentResource.energy;
        protected float maxResource => currentResource.currentEnergy;
        [SerializeField] protected RectTransform rectTransform;

        [Inject]
        public override void GetMoney(PlayerResources[] r)
        {
            playerResources = r;
            ID = 0;
            OnEnable();
            SetCount();
        }

        protected override void SetCount()
        {
            float current = Mathf.Clamp(resource / maxResource, 0.01f, 1); 
            rectTransform.localScale = new Vector3(1, 1, current != float.NaN ? current : 0);
        }

        private void OnEnable()
        {
            if (currentResource == null) return;
            currentResource.EnergyEvent += SetCount;
        }

        private void OnDestroy() =>
            currentResource.EnergyEvent -= SetCount;
    }
}
