using UnityEngine;
using Zenject;

namespace RTS
{
    public class UnitButtonFactory : IFactory<GameObject, UnitButtonStruct, GameObject> // Factory for create buttons
    {
        private readonly DiContainer container;

        public UnitButtonFactory(DiContainer c) =>
            container = c;

        public GameObject Create(GameObject bt, UnitButtonStruct str)
        {
            GameObject b = container.InstantiatePrefab(bt, str.parent);
            b.GetComponent<IBUttonInit>().Init(str.id, str.builder, str.stats);

            return b;
        }
    }
}