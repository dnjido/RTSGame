using UnityEngine;
using Zenject;

namespace RTS
{
    public class ProjectileFactory : IFactory<GameObject, ProjectileTransform, GameObject> // Factory for spawning projectiles
    {
        private readonly DiContainer container;

        public ProjectileFactory(DiContainer c) => 
            container = c;

        public GameObject Create(GameObject p, ProjectileTransform t)
        {
            GameObject obj = container.InstantiatePrefab(p);
            obj.GetComponent<IProjectileConstruct>().Construct(t);
            return obj;
        }
    }
}
