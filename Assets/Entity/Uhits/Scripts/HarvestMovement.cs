using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class HarvestMovement : UnitMovement // Moving a unit to a mine or to plant.
    {
        [SerializeField] public GameObject currentMine, currentPlant;

        protected override void Start()
        {
            base.Start(); 
            
            StartCoroutine(HarvestStartCoroutine());
        }

        private IEnumerator HarvestStartCoroutine()
        {
            yield return new WaitForSeconds(Time.deltaTime);
            RefreshPlant();
            RefreshMine();
        }

        public void RefreshMine() 
        {
            currentMine = SearchMine();
            if (!currentMine) { SetUnit(currentPlant); return; }
            SetMine(currentMine);
        } 

        public void RefreshPlant()
        {
            currentPlant = SearchPlant();
            if (!currentPlant) return;
            SetPlant(currentPlant);
        }

        private void SetMine(GameObject m)
        {
            updater = new MineState(agent, m, gameObject, currentPlant);
            SetHarvestUnit(m, SearchMine());
        }

        private void SetPlant(GameObject m)
        {
            updater = new PlantState(agent, m, gameObject, currentMine);
            SetHarvestUnit(m, SearchPlant());
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

            currentMine.GetComponent<IHarvest>().currentHarvester = null;
            base.SetUnit(u);
        }

        public override void SetPoint(Vector3 p)
        {
            base.SetPoint(p);
            if (!currentMine) return;

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

        private GameObject SearchMine()
        {
            GameObject[] mines = GameObject.FindGameObjectsWithTag("Unit");
            List<GameObject> minesL = new List<GameObject>();
            if (mines.Length <= 0) return null;

            foreach (GameObject mine in mines) 
            {
                if (GetUnitType.Type("Ore", mine.GetComponent<UnitFacade>())) 
                    minesL.Add(mine);
            }

            if (minesL.Count <= 0) return null;

            List<GameObject> minesT = new List<GameObject>();
            foreach (GameObject mine in minesL)
            {
                if(mine.GetComponent<Mine>().GetOre() > 0)
                    minesT.Add(mine);
            }
            minesL = minesT;

            if (minesL.Count <= 0) return null;

            GameObject nearMine = FindNearest.FindObject(minesL.ToArray(), transform.position);

            return nearMine;
        }

        private GameObject SearchPlant()
        {
            GameObject[] plants = GameObject.FindGameObjectsWithTag("Unit");
            List<GameObject> plantsL = new List<GameObject>();
            if (plants.Length <= 0) return null;

            foreach (GameObject plant in plants) 
            {
                if (GetUnitType.Type("Plant", plant.GetComponent<UnitFacade>()))
                    plantsL.Add(plant);
            }

            if (plantsL.Count <= 0) return null;

            List<GameObject> plantsT = new List<GameObject>();
            foreach (GameObject plant in plantsL)
            {
                if (plant.GetComponent<UnitTeam>().team == GetComponent<UnitTeam>().team)
                    plantsT.Add(plant);
            }
            plantsL = plantsT;

            GameObject nearMine = FindNearest.FindObject(plantsL.ToArray(), transform.position);

            return nearMine;
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
                    //if (facade.GetComponent<>() == "Unit")
                    return true;
                }
            }
            return false;
        }
    }
}
