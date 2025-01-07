using RTS;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class StartMenuInstaller : MonoInstaller
{
    public StartGameProperties startGameProperties;

    public override void InstallBindings()
    {
        //Container.Bind<StartGameProperties>().AsCached();
        Container.BindInstances(startGameProperties);
    }
}