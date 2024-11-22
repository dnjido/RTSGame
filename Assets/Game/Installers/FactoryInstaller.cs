using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace RTS 
{
    public class FactoryInstaller : MonoInstaller
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject shell;

        //public override void InstallBindings()
        //{
        //    var projectilesPrefabs = new Dictionary<string, GameObject>
        //{
        //    { "Bullet", bullet },
        //    { "Shell", shell }
        //};
        //
        //    Container.Bind<Dictionary<string, GameObject>>().FromInstance(projectilesPrefabs).AsSingle();
        //    Container.BindFactory<IEnemy, EnemyFactory>().FromFactory<CustomEnemyFactory>();
        //    //Container.Bind<IProjectileFactory>().To<ProjectileFactory>().AsTransient();
        //}
        public override void InstallBindings()
        {
            Container.BindFactory<GameObject, ProjectileTransform, GameObject, ProjectileFacade.Factory>().FromFactory<ProjectileFactory>();
            Container.BindFactory<GameObject, UnitTransform, GameObject, UnitFacade.Factory>().FromFactory<UnitFactory>();
        }
    }
}
