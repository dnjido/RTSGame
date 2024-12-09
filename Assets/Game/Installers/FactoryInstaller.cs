using UnityEngine;
using Zenject;

namespace RTS 
{
    public class FactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<GameObject, ProjectileTransform, GameObject, ProjectileFacade.Factory>().FromFactory<ProjectileFactory>();
            Container.BindFactory<GameObject, UnitTransform, GameObject, UnitFacade.Factory>().FromFactory<UnitFactory>();
            Container.BindFactory<GameObject, UnitButtonStruct, GameObject, ButtonFacade.Factory>().FromFactory<UnitButtonFactory>();
        }
    }
}
