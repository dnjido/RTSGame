using RTS;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AttackArmorInstaller", menuName = "Installers/AttackArmorInstaller")]
public class AttackArmorInstaller : ScriptableObjectInstaller<AttackArmorInstaller>
{
    public AttackTypeStats[] attackReduced;
    public override void InstallBindings()
    {
        Container.BindInstances(attackReduced);
    }
}