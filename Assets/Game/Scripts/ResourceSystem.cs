using System;
using UnityEngine;

namespace RTS
{
    [Serializable]
    public class PlayerResources // Manages resources
    {
        public float money;
        public float energy, currentEnergy;
        public bool power;

        public delegate void MoneyDelegate();
        public event MoneyDelegate MoneyEvent;

        public delegate void EnergyDelegate();
        public event EnergyDelegate EnergyEvent;

        public delegate void PowerDelegate(bool e);
        public event PowerDelegate PowerEvent;

        public void ChangeMoney(float count)
        {
            money = Mathf.Clamp(money + count, 0, int.MaxValue);
            MoneyEvent?.Invoke();
        }

        public bool CheckAndChange(float count)
        {
            if (money < -count) return false;

            ChangeMoney(count);
            return true;
        }

        public void ChangeEnergy(float count)
        {
            energy += count;
            EnergyInvoke();
        }

        public void ChangeMaxEnergy(float count)
        {
            currentEnergy += count;
            EnergyInvoke();
        }

        public void EnergyInvoke()
        {
            EnergyEvent?.Invoke();
            ChangePower();
        }

        public void ChangePower() =>
            PowerEvent?.Invoke(energy < currentEnergy);
    }
}
