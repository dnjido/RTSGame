using Zenject;

namespace RTS
{
    public class SelectingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SelectedUnits>().AsSingle();
            Container.Bind<MoveCommand>().AsSingle();
            Container.Bind<SelectionBorder>().FromComponentInHierarchy().AsSingle();
        }
    }
}
