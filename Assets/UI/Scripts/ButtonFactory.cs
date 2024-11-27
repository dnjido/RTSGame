using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RTS
{
    public class UnitButtonFactory : IFactory<GameObject, UnitButtonStruct, GameObject[]> // Factory for create buttons
    {
        private readonly DiContainer container;

        public UnitButtonFactory(DiContainer c) =>
            container = c;

        public GameObject[] Create(GameObject bt, UnitButtonStruct str)
        {
            List<GameObject> buttons = new List<GameObject>();
            for (int i = 0; i < str.count; i++)
            {
                GameObject b = container.InstantiatePrefab(bt, str.parent);
                b.GetComponent<BuildCommand>().SetBuilder(i, str.builder);
                buttons.Add(b);
            }
            return buttons.ToArray();
        }

        public void Clear(Transform p)
        {
            //foreach (Transform child in p.transform)
            //    child.gameObject.Destroy(child.gameObject);
        }
    }
}