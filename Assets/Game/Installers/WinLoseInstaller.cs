using RTS;
using UnityEngine;
using Zenject;

public class WinLoseInstaller : MonoInstaller
{
    [SerializeField]public HasPlaying playerPlaying;

    [Inject]
    public void SetPlaying(Relationship[] r, EndGameText end) => 
        playerPlaying.Init(this, r, end);

    public override void InstallBindings()
    {
        Container.BindInstances(playerPlaying);
    }
}