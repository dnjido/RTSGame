using RTS;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Zenject;

public class StartPlayer : MonoBehaviour
{
    [SerializeField] private GameObject startBuild;
    [SerializeField] private GameObject[] startUnits;

    private UnitFacade.Factory unitFactory;

    public StartPoint start;

    [Inject]
    public void UnitFactory(UnitFacade.Factory f) => 
        unitFactory = f;

    private void Start() => Place();

    public void Place()
    {
        if (unitFactory == null || start.team == 0) return;
        GameObject unit = Spawn(startBuild);
    }

    private GameObject Spawn(GameObject unit)
    {
        UnitTransform tr = SetUnit.Create(
            transform.position,
            transform.rotation,
            start.team);
        return unitFactory.Create(unit, tr);
    }
}
