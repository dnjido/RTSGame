using UnityEngine;
using System.Collections;
using Zenject;

namespace RTS
{
    public class RepairBuild : MonoBehaviour
    {
        [SerializeField] private float count, delay;
        private PlayerResources playerResources;
        private bool repaired;

        private float cost => GetComponent<UnitFacade>().cost;
        private int team => GetComponent<UnitTeam>().team;
        private HealthSystem health => GetComponent<HealthSystem>();
        private float maxHealth => GetComponent<HealthSystem>().MaxHealth();
        private float curHealth => GetComponent<HealthSystem>().GetHealth();
        private bool canRepair => !(!playerResources.CheckAndChange(count / maxHealth * -cost) || curHealth >= maxHealth);

        public delegate void RepairDelegate(bool r);
        public event RepairDelegate RepairEvent;

        [Inject]
        public void GetMoney(PlayerResources[] r)
        {
            playerResources = r[team - 1];
        }

        public void Begin(int t)
        {
            if (team != t || !canRepair) return;
            if (repaired) { Stop(); return; }

            repaired = true;
            StartCoroutine(Repair());
            RepairEvent?.Invoke(true);
        }

        private IEnumerator Repair()
        {
            yield return new WaitForSeconds(delay);
            Apply();
        }

        private void Apply()
        {
            if (!repaired) return;
            if (!canRepair) { Stop(); return; }

            health.Healing(cost);
            StartCoroutine(Repair());
        }

        private void Stop()
        {
            repaired = false;
            RepairEvent?.Invoke(false);
        }
    }
}
