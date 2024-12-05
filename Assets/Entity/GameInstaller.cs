using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace TBS
{
    public class GameInstaller : MonoInstaller
    {
        public GameObject bullet;
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ProjMoveBus>();
            // Связываем Weapon как Singleton
            Container.Bind<Player>().AsTransient();

            Container.Bind<Weapon>().AsSingle();

            Container.Bind<ProjectileMove>().FromComponentsInHierarchy().AsTransient();

            Container.BindSignal<ProjMoveBus>().ToMethod<ProjectileMove>(x => x.MoveBus).FromResolve();

            Container.Bind<Projectile>().AsTransient();

            Container.Bind<ProjectileCreator>().FromComponentInHierarchy().AsTransient();

            Container.BindFactory<BulletBase, ProjectileCreator.Factory>().FromComponentInNewPrefab(bullet).UnderTransformGroup("Bullets");//.WithGameObjectName("Bullet")
        }
    }
}