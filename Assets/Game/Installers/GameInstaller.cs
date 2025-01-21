using System.Linq;
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
            playerRelationships.Select((r, i) => 
                { r.SetRelationship(playerRelationships, i); return r; });
        }

        public override void InstallBindings()
        {
            Container.BindInstances(playerResources);
            Container.BindInstances(playerFabrics);
            Container.BindInstances(playerRelationships);
        }
    }
}