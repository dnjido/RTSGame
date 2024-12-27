using Zenject;

namespace RTS
{
    public class GameInstaller : MonoInstaller // Storage amount of player resources
    {
        public PlayerResources[] playerResources = new PlayerResources[8];
        public FabricsList[] playerFabrics = new FabricsList[8];
        public Relationship[] playerRelationships = new Relationship[8];

        public void SetRelationship()
        {
            int i = 0;
            foreach (var r in playerRelationships)
            {
                r.SetRelationship(playerRelationships, i);
                i++;
            }
        }

        public override void InstallBindings()
        {
            Container.BindInstances(playerResources);
            Container.BindInstances(playerFabrics);
            SetRelationship();
            Container.BindInstances(playerRelationships);
        }
    }
}