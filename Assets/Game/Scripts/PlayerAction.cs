using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public interface IAction
    {
        void Action(GameObject unit);
    }

    public class PlayerAction
    {
        int team;
        IAction action;
        
        public void Set(IAction a)
        {
            action = a;
        }
        
        public void Apply(GameObject unit) =>
            action?.Action(unit);

        public void Cancel() =>
            action = null;
    }



    public class SellAction : IAction
    {
        private readonly int team;

        public SellAction(int t) => team = t;

        public void Action(GameObject unit) =>
            unit.GetComponent<SellBuild>()?.Sell(team);
    }

    public class RepairAction : IAction
    {
        private readonly int team;

        public RepairAction(int t) => team = t;

        public void Action(GameObject unit) =>
            unit.GetComponent<RepairBuild>()?.Begin(team);
    }
}
