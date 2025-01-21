using RTS;
using UnityEngine;
using Zenject;

public class StartGameInstaller : MonoInstaller
{
    [SerializeField] private StartGame startGame;

    [Inject]
    public void Set([InjectOptional] StartGameProperties sg, AIManadger[] AI, Relationship[] r, HasPlaying p)
    {
        if (sg == null) startGame.InitEmpty(AI, r, p);
        else startGame.Init(sg, AI, r, p);
    }

    public override void InstallBindings()
    {        
        Container.BindInstances(startGame);
        //startGame.Start();
    }
}