using UnityEngine;
using Zenject;

namespace RTS
{
    public class EnergyConsumption : MonoBehaviour //Struct of units characteristics
    {
        [SerializeField] private float energy;
        private int team => GU.Team(gameObject);

        [SerializeField] private bool powerDisable;

        protected PlayerResources[] playerResources;
        protected PlayerResources currentResource => playerResources[team - 1];

        [Inject]
        public void UnitStats(GetStats g)
        {
            energy = g.Stats(gameObject).generalStats.energy;
        }

        [Inject]
        public void GetEnergy(PlayerResources[] r)
        {
            playerResources = r;
        }

        public void Activate()
        {
            OnEnable();
            SetEnergy(1);
        }

        private void Start() => SetEnergy(1);

        public void SetEnergy(int mult)
        {
            if (energy <= 0) currentResource.ChangeEnergy(-energy * mult);
            else currentResource.ChangeMaxEnergy(energy * mult);
            currentResource.ChangePower();
        }

        private void Disable(bool b)
        {
            if (this == null) return;

            GetComponent<Attack>()?.SetStop(!b);
            GetComponent<DetectEnemy>()?.SetStop(!b);
        }

        private void OnEnable()
        {
            if (currentResource == null) return;
            currentResource.PowerEvent += Disable;
        }

        private void OnDisable()
        {
            SetEnergy(-1);
            currentResource.PowerEvent -= Disable;
        }
    }      
}
