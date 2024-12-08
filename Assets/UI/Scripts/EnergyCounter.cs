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
            currentResource.EnergyEvent += SetCount;
            SetCount();
        }

        protected override void SetCount()
        { 
            if(resource == 0) return;
            float current = Mathf.Clamp(resource / maxResource, 0.01f, 1);
            rectTransform.localScale = new Vector2(1, current);
        }

        private void OnDestroy() =>
            currentResource.EnergyEvent -= SetCount;
    }
}
