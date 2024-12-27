using RTS;
using UnityEngine;
using Zenject;

public class AIInstaller : MonoInstaller // Initialization AI and make command
{
    private FabricsList[] builders;
    private PlayerResources[] resources;

    public AIManadger[] AIs = new AIManadger[8];

    [Inject]
    public void GetBuilder(FabricsList[] b) =>
        builders = b;

    [Inject]
    public void GetResources(PlayerResources[] r) =>
        resources = r;

    public void SetAI()
    {
        int i = 0;
        foreach (var ai in AIs)
        {
            if(ai.isAI)
            {
                ai.monoBehaviour = this;
                ai.SetTeam(i);
                ai.Init(builders[i], resources[i]);
            }
            i++;
        }
    }

    public override void InstallBindings()
    {
        SetAI();
        Container.BindInstances(AIs);
    }
}