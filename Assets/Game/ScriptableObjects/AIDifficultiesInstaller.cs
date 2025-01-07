using RTS;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AIDifficulty", menuName = "Installers/AIDifficulty")]
public class AIComplexity : ScriptableObjectInstaller<AIComplexity>
{
    public AIDifficulty[] aiDifficulty;
    public override void InstallBindings()
    {
        Container.BindInstances(aiDifficulty);
    }
}