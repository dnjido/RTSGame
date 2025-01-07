using UnityEngine;
using Zenject;

namespace RTS 
{
    public class UIFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GenericUIFactory>().AsTransient();
        }
    }
}
