using RTS;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterSearching
{
    GameObject unit;

    public HarvesterSearching(GameObject u)
    {
        unit = u;
    }

    public GameObject SearchMine() // Harvester is searching mine or to plant.
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
            if (mine.GetComponent<Mine>().GetOre() > 0)
                minesT.Add(mine);
        }
        minesL = minesT;

        if (minesL.Count <= 0) return null;

        GameObject nearMine = FindNearest.FindObject(minesL.ToArray(), unit.transform.position);

        return nearMine;
    }

    public GameObject SearchPlant()
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
            if (plant.GetComponent<UnitTeam>().team == unit.GetComponent<UnitTeam>().team)
                plantsT.Add(plant);
        }
        plantsL = plantsT;

        GameObject nearMine = FindNearest.FindObject(plantsL.ToArray(), unit.transform.position);

        return nearMine;
    }
}
