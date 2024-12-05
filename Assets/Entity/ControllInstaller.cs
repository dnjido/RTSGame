using UnityEngine;
using Zenject;

public class ControllInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LookAtCursor>().AsTransient();
    }
}