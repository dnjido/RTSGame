using RTS;
using UnityEngine;
using Zenject;

public class MonoBehaviourInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<DetectEnemy>().FromComponentInHierarchy().AsTransient();
        Container.Bind<RotateBody>().FromComponentInHierarchy().AsTransient();
        //Container.Bind<DetectEnemy>().To<UnitMovement>().AsTransient();
        //Container.Bind<Bullet>().WithId("ServiceB");
    }
}