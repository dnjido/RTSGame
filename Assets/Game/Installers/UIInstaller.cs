using Zenject;

public class UIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SidePanel>().FromComponentInHierarchy().AsSingle();
    }
}