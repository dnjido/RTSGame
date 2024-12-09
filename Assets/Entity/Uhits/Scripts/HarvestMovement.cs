using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public class HarvestMovement : UnitMovement // Moving a unit to a mine or to plant.
    {
        [SerializeField] public GameObject currentMine, currentPlant;
        public NavMeshAgent agent { get; set; }

        private HarvesterSearching searching;

        protected override void Start()
        {
            base.Start();

            GetComponent<NavMeshAgent>().speed = speed;

            searching = new HarvesterSearching(gameObject);

            StartCoroutine(HarvestStartCoroutine());
        }

        private IEnumerator HarvestStartCoroutine()
        {
            yield return new WaitForSeconds(Time.deltaTime);
            RefreshPlant();
            RefreshMine();
        }

        private void SetMine(GameObject m)
        {
            updater = new MineState(gameObject, new NavMeshMove(gameObject), m, currentPlant);
            SetHarvestUnit(m, searching.SearchMine());
        }

        private void SetPlant(GameObject m)
        {
            updater = new PlantState(gameObject, new NavMeshMove(gameObject), m, currentMine);
            SetHarvestUnit(m, searching.SearchPlant());
        }

        protected override void SetUnit(GameObject u)
        {
            moveStruct.unit = u;
            UnitFacade facade = u.GetComponent<UnitFacade>();

            if (GetUnitType.Type("Ore", facade))
            {
                currentMine = u;
                SetMine(u);
                return; 
            }
            if (GetUnitType.Type("Plant", facade))
            {
                currentPlant = u;
                SetPlant(u);
                return;
            }
            base.SetUnit(u);

            currentMine.GetComponent<IHarvest>().currentHarvester = null;
        }

        public override void SetPoint(Vector3 p)
        {
            base.SetPoint(p);

            currentMine.GetComponent<IHarvest>().currentHarvester = null;
            currentMine = null;
        }

        private void SetHarvestUnit(GameObject getU, GameObject setU)
        {
            if (!getU)
            {
                GameObject unit = setU;
                if (unit) getU = unit;
            }
        }

        public void MoveTo(GameObject u) => SetUnit(u);

        public void RefreshMine()
        {
            currentMine = searching.SearchMine();
            if (!currentMine) { SetUnit(currentPlant); return; }
            SetMine(currentMine);
        }

        public void RefreshPlant()
        {
            currentPlant = searching.SearchPlant();
            if (!currentPlant) return;
            SetPlant(currentPlant);
        }
    }

    public class GetUnitType
    {
        public static bool Type(string type, UnitFacade facade)
        {
            foreach (string t in facade.unitType)
            {
                if (t == type)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
