using RTS;
using UnityEngine;
using Zenject;

public class AIInstaller : MonoInstaller // Initialization AI and make command
{
    private FabricsList[] builders;
    private PlayerResources[] resources;
    private AIDifficulty[] difficulties;

    public AIManadger[] AIs = new AIManadger[8];

    [Inject]
    public void GetBuilder(FabricsList[] b) =>
        builders = b;

    [Inject]
    public void GetResources(PlayerResources[] r) =>
        resources = r;

    [Inject]
    public void Difficulty(AIDifficulty[] ai) =>
        difficulties = ai;

    public void SetAI()
    {
        int i = 0;
        foreach (AIManadger ai in AIs)
        {
            ai.monoBehaviour = this;
            ai.SetTeam(i);
            ai.Init(builders[i], resources[i], difficulties);
            i++;
        }
    }

    public override void InstallBindings()
    {
        SetAI();
        Container.BindInstances(AIs);
    }
}