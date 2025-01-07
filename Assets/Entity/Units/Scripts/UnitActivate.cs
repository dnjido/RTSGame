using RTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class UnitActivate : MonoBehaviour
    {
        [SerializeField] private bool acivate = true;
        private bool init;

        private void Start()
        {
            bool act = GetComponent<Placing>().hasPlaced;
            
            //if (!act) { Activate(); }
            if (act) { Deactivate(); }
        }

        public void Toggle()
        {
            if (!acivate) { Activate(); }
            if (acivate) { Deactivate(); }
        }

        public void Activate()
        {
            GetComponent<UnitTeam>()?.SetLayer();
            GetComponent<EnergyConsumption>()?.SetEnergy(1);
            GetComponent<FogClear>()?.Make();
            GetComponent<SetBuildList>()?.SetList();
            GetComponent<HideUnit>()?.StartClear(true); 
            GetComponent<Attack>()?.SetStop(false);
            GetComponent<DetectEnemy>()?.SetStop(false);
            GetComponent<SpawnUnitAtStart>()?.Activate();
            acivate = true;
        }

        public void Deactivate()
        {
            GetComponent<UnitTeam>()?.RemoveLayer();
            GetComponent<EnergyConsumption>()?.SetEnergy(-1);
            GetComponent<FogClear>()?.Remove();
            GetComponent<SetBuildList>()?.Destroy();
            GetComponent<HideUnit>()?.StartClear(false);
            GetComponent<Attack>()?.SetStop(true);
            GetComponent<DetectEnemy>()?.SetStop(true);
            GetComponent<SpawnUnitAtStart>()?.Deactivate();
            acivate = false;
        }

        private void OneTimeAtStart()
        {
            if (init) return;

            init = true;
        }
    }
}
