using UnityEngine;
using Zenject;

namespace RTS
{
    public class SetBuildList : MonoBehaviour // Add or remove build from building list
    {
        protected FabricsList[] fabricList;

        public int team => GetComponent<UnitTeam>().team;

        [Inject]
        public void FabricsList(FabricsList[] f) =>
            fabricList = f;

        public void SetList() =>
            fabricList[team - 1].Add(gameObject);

        public void Destroy() =>
            fabricList[team - 1].Remove(gameObject);

        private void OnEnable()
        {
            GetComponent<HealthSystem>().DeathEvent += Destroy;
        }

        private void OnDisable()
        {
            GetComponent<HealthSystem>().DeathEvent -= Destroy;
        }
    }
}
