using RTS;
using UnityEngine;
using Zenject;

public class StartGameInstaller : MonoInstaller
{
    [SerializeField] private StartGame startGame;

    [Inject]
    public void Set([InjectOptional] StartGameProperties sg)
    {
        if (sg == null) startGame.startGameProperties = new StartGameProperties();
        else startGame.startGameProperties = sg;
    }

    [Inject]
    public void Set2(AIManadger[] AI) => startGame.AI = AI;

    [Inject]
    public void Set3(Relationship[] relationships) => 
        startGame.relationships = relationships;

    [Inject]
    public void Set4(HasPlaying p) =>
        startGame.hasPlaying = p;

    public override void InstallBindings()
    {
        
        Container.BindInstances(startGame);
        startGame.Start();
    }
}