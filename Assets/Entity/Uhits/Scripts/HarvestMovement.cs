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
            currentMine = SearchMine();
            currentPlant = SearchPlant();
            SetUnit(SearchMine());
        }

        protected override void SetUnit(GameObject u)
        {
            moveStruct.unit = u;
            UnitFacade facade = u.GetComponent<UnitFacade>();

            if (GetUnitType.Type("Ore", facade))
            {
                updater = new MineState(agent, u, gameObject);
                SetHarvestUnit(currentMine, SearchMine());
                return; 
            }
            if (GetUnitType.Type("Plant", facade))
            {
                updater = new PlantState(agent, u, gameObject);
                SetHarvestUnit(currentPlant, SearchPlant());
                return;
            }

            base.SetUnit(u);
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

            foreach (GameObject mine in minesL)
            {
                if(mine.GetComponent<Mine>().empty) 
                    minesL.Remove(mine);
            }

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

            foreach (GameObject plant in plantsL)
            {
                if(plant.GetComponent<UnitTeam>().team != GetComponent<UnitTeam>().team)
                    plantsL.Remove(plant);
            }

            if (plantsL.Count <= 0) return null;

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
