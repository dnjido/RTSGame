using UnityEngine;

namespace RTS
{
    public interface IDamage
    {
        public void ApplyDamage(float count);
    }

    public interface IHealing
    {
        public void Healing(float count);
    }

    public interface IDeath
    {
        public void Death();
    }

    public class HealthSystem : MonoBehaviour, IDamage, IHealing, IDeath
    {
        [SerializeField] private float maxHealth, currentHealth;
        [SerializeField] private float armor;

        public delegate void DamageDelegate(float health);
        public event DamageDelegate DamageEvent;

        public delegate void DeathDelegate();
        public event DeathDelegate DeathEvent;

        void Start() => currentHealth = maxHealth;

        public void ApplyDamage(float count)
        {
            DamageEvent?.Invoke(HealthCalc.GetPercent(currentHealth, maxHealth));
            currentHealth = HealthCalc.Change(-count, currentHealth, armor);
            if (HealthCalc.GetPercent(currentHealth, maxHealth) <= 0) Death();
        }

        public void Healing(float count) =>
            HealthCalc.Change(count, currentHealth, 0);

        public void Death() 
        {
            DeathEvent?.Invoke();
            Destroy(gameObject);
        }
    }

}