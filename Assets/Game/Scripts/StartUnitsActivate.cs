using RTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class StartUnitsActivate : MonoBehaviour
{
    private HasPlaying hasPlaying;

    [Inject]
    private void Init(HasPlaying hp) =>
        hasPlaying = hp;

    void Start()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

        foreach (GameObject unit in units)
        {
            if (!unit.GetComponent<UnitActivate>()) continue;
            unit.GetComponent<UnitActivate>().Activate();
        }

        hasPlaying.EnableSetStatus(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
