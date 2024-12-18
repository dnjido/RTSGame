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

        protected int mult = 1;

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

        public void SetEnergy()
        {
            currentResource.PowerEvent += Disable;
            //SetEnergy();
            if (energy <= 0) currentResource.ChangeEnergy(-energy * mult);
            else currentResource.ChangeMaxEnergy(energy * mult);
            currentResource.ChangePower();
        }

        private void Disable(bool b)
        {
            GetComponent<Attack>()?.SetStop(!b);
            GetComponent<DetectEnemy>()?.SetStop(!b);
        }

        private void OnDestroy()
        {
            mult = -1;
            SetEnergy();
            currentResource.PowerEvent -= Disable;
        }
    }      
}
