using System;
using UnityEngine;

namespace RTS
{
    [Serializable]
    public class UnitsStats //Struct of units characteristics
    {
        public GeneralStats generalStats;
        public AttackStats attackStats;
        public HealthStats healthStats;
        public MoveStats moveStats;
        public BuildStats buildStats;
        public HarvestingStats harvestingStats;
    }

    [Serializable]
    public struct GeneralStats
    {
        public string name;
        public int buildTime;
        public float cost;
        public float energy;
        public float costPerTick => cost / (buildTime / Time.deltaTime);
    }

    [Serializable]
    public struct AttackStats
    {
        public float damage;
        public float range;
        public float attackRate;
        public GameObject projectile;
    }

    [Serializable]
    public struct HealthStats
    {
        public float health;
        public float armor;
    }

    [Serializable]
    public struct MoveStats
    {
        public float speed;
        public float rotate;
    }

    [Serializable]
    public struct BuildStats
    {
        public GameObject buttons;
        public GameObject[] units;
        public GameObject[] secondUnits;
    }

    [Serializable]
    public struct HarvestingStats
    {
        public float maxLoad;
        public float grub;
        public float delay;
    }
}
