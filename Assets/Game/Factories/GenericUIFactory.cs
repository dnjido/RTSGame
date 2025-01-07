using RTS;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GenericUIFactory
{
    private readonly DiContainer container;

    public GenericUIFactory(DiContainer c) =>
            container = c;

    public GameObject Create<T>(GameObject prefab, T param1)
    {
        GameObject ui = container.InstantiatePrefab(prefab);
        ui.GetComponent<IUICreate<T>>().Create(param1);

        return ui;
    }

    public GameObject Create<T>(GameObject prefab, T param1, T param2)
    {
        GameObject ui = container.InstantiatePrefab(prefab);
        ui.GetComponent<IUICreate<T, T>>().Create(param1, param2);
    
        return ui;
    }

    public GameObject Create(GameObject prefab)
    {
        GameObject ui = container.InstantiatePrefab(prefab);
        ui.GetComponent<IUICreate>().Create();

        return ui;
    }
}
