using UnityEngine;
using Zenject;
using RTS;

[CreateAssetMenu(fileName = "MapsInstaller", menuName = "Installers/MapsInstaller")]
public class MapsInstaller : ScriptableObjectInstaller<MapsInstaller>
{
    public MapProperties[] mapProperties;

    public override void InstallBindings()
    {
        Container.BindInstances(mapProperties);
    }
}