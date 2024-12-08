using Zenject;

namespace RTS
{
    public class GameInstaller : MonoInstaller // Storage amount of player resources
    {
        public PlayerResources[] playerResources = new PlayerResources[8];
        public override void InstallBindings()
        {
            Container.BindInstances(playerResources);
        }
    }
}