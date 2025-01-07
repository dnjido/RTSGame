using RTS;
using System.Collections.Generic;
using System.Linq;
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
        try {
            GameObject[] mines = GameObject.FindGameObjectsWithTag("Unit");

            List<GameObject> validMines = mines
                .Where(mine => GetUnitType.Type("Ore", mine.GetComponent<UnitFacade>()))
                .Where(mine => mine.GetComponent<Mine>().GetOre() > 0)
                .ToList();

            GameObject nearMine = FindNearest.FindObject(validMines.ToArray(), unit.transform.position);

            return nearMine;
        }
        catch { return null; }
        
    }

    public GameObject SearchPlant()
    {
        try
        {
            GameObject[] plants = GameObject.FindGameObjectsWithTag("Unit");

            List<GameObject> validPlants = plants
                .Where(plant => GetUnitType.Type("Plant", plant.GetComponent<UnitFacade>()))
                .Where(plant => plant.GetComponent<UnitTeam>().team == unit.GetComponent<UnitTeam>().team)
                .ToList();

            GameObject nearPlant = FindNearest.FindObject(validPlants.ToArray(), unit.transform.position);

            return nearPlant;
        }
        catch { return null; }
    }
}

//public GameObject SearchMine() // Harvester is searching mine or to plant.
//{
//    GameObject[] mines = GameObject.FindGameObjectsWithTag("Unit");
//    List<GameObject> minesL = new List<GameObject>();
//    if (mines.Length <= 0) return null;
//    
//    foreach (GameObject mine in mines)
//    {
//        if (GetUnitType.Type("Ore", mine.GetComponent<UnitFacade>()))
//            minesL.Add(mine);
//    }
//    
//    if (minesL.Count <= 0) return null;
//    
//    List<GameObject> minesT = new List<GameObject>();
//    foreach (GameObject mine in minesL)
//    {
//        if (mine.GetComponent<Mine>().GetOre() > 0)
//            minesT.Add(mine);
//    }
//    minesL = minesT;
//    
//    if (minesL.Count <= 0) return null;
//    
//    GameObject nearMine = FindNearest.FindObject(minesL.ToArray(), unit.transform.position);
//    
//    return nearMine;
//}

//public GameObject SearchPlant()
//{
//    GameObject[] plants = GameObject.FindGameObjectsWithTag("Unit");
//    List<GameObject> plantsL = new List<GameObject>();
//    if (plants.Length <= 0) return null;
//
//    foreach (GameObject plant in plants)
//    {
//        if (GetUnitType.Type("Plant", plant.GetComponent<UnitFacade>()))
//            plantsL.Add(plant);
//    }
//
//    if (plantsL.Count <= 0) return null;
//
//    List<GameObject> plantsT = new List<GameObject>();
//    foreach (GameObject plant in plantsL)
//    {
//        if (plant.GetComponent<UnitTeam>().team == unit.GetComponent<UnitTeam>().team)
//            plantsT.Add(plant);
//    }
//    plantsL = plantsT;
//
//    GameObject nearMine = FindNearest.FindObject(plantsL.ToArray(), unit.transform.position);
//
//    return nearMine;
//}
