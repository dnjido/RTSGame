using UnityEngine;
using Zenject;

namespace RTS
{
    public class UnitFactory : IFactory<GameObject, UnitTransform, GameObject> // Factory for spawning projectiles
    {
        private readonly DiContainer container;

        public UnitFactory(DiContainer c) => 
            container = c;

        public GameObject Create(GameObject p, UnitTransform t)
        {
            GameObject obj = container.InstantiatePrefab(p);
            obj.GetComponent<IUnitConstruct>().Construct(t);
            return obj;
        }
    }
}
