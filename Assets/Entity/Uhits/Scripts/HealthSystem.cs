using System;
using UnityEngine;
using Zenject;

namespace RTS
{
    public interface IDamage
    {
        public void ApplyDamage(float count, AttackType attackType);
        public void Healing(float count);
        public void Death();
    }

    public class HealthSystem : MonoBehaviour, IDamage
    {
        [SerializeField] private float maxHealth, armor;
        [SerializeField] private ArmorType armorType;
        [SerializeField] private float currentHealth;

        private ArmorTypeStats[] attackReduced;

        public delegate void DamageDelegate(float health);
        public event DamageDelegate DamageEvent;

        public delegate void DeathDelegate();
        public event DeathDelegate DeathEvent;

        public float percent => currentHealth / maxHealth;

        void Start() => currentHealth = maxHealth;

        public float GetHealth() => currentHealth;
        public float MaxHealth() => maxHealth;

        [Inject]
        public void UnitStats(GetStats g)
        {
            maxHealth = g.Stats(gameObject).healthStats.health;
            armor = g.Stats(gameObject).healthStats.armor;
            armorType = g.Stats(gameObject).healthStats.armorType;
        }

        [Inject]
        public void SetReduce(AttackTypeStats[] r)
        {
            attackReduced = r[(int)armorType].attack;
        }

        public void ApplyDamage(float count, AttackType attackType)
        {
            DamageEvent?.Invoke(percent);

            count *= attackReduced[(int)attackType].reduce;
            currentHealth = HealthCalc.Change(-count, currentHealth, armor);

            if (percent <= 0) Death();
        }

        public void Healing(float count)
        {
            DamageEvent?.Invoke(percent);
            currentHealth = HealthCalc.Change(count, currentHealth, 0);
        }

        public void Death() 
        {
            DeathEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}