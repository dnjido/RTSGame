using System;
using UnityEditor;

namespace RTS
{
    public enum AttackType
    {
        Bullet = 0,
        Rocket = 1,
        Shell = 2,
    };
    public enum ArmorType
    {
        Light = 0,
        Medium = 1,
        Heavy = 2,
        Build = 3,
    };

    [Serializable]
    public struct ArmorTypeStats // Struct of units characteristics
    {
        public float reduce;
    }

    [Serializable]
    public class AttackTypeStats //List of damage reduced
    {
        public ArmorTypeStats[] attack = new ArmorTypeStats[3];
    }
}
