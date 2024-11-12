using UnityEngine;
using Zenject;

namespace RTS
{
    public class SelectingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SelectedUnits>().AsSingle();
        }
    }
}
