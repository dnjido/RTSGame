using RTS;
using UnityEngine;
using Zenject;

public class WinLoseInstaller : MonoInstaller
{
    [SerializeField]public HasPlaying playerPlaying;

    [Inject]
    public void SetPlaying(Relationship[] relationship) =>
        playerPlaying.relationship = relationship;

    [Inject]
    public void SetUI(EndGameText end) =>
        playerPlaying.gameStatusUI = end;

    public override void InstallBindings()
    {
        Container.BindInstances(playerPlaying);
        playerPlaying.monoBehaviour = this;
        //Container.Bind<HasPlaying>();
    }
}