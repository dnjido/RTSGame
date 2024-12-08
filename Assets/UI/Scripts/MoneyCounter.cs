using TMPro;
using UnityEngine;
using Zenject;

namespace RTS
{
    public class MoneyCounter : Counters // Displays amount of money
    {
        protected override float resource => currentResource.money;
        protected TMP_Text counter;

        [Inject]
        public override void GetMoney(PlayerResources[] r)
        {
            playerResources = r;
            ID = 0;
            currentResource.MoneyEvent += SetCount;
            counter = GetComponent<TMP_Text>();
            SetCount();
        }

        protected override void SetCount() =>
            counter.text = Mathf.Floor(resource).ToString() + "$";

        private void OnDestroy() =>
            currentResource.MoneyEvent -= SetCount;
    }
}
