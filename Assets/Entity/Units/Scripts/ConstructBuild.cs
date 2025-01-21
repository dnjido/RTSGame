using UnityEngine;
using System.Linq;
using System;
using System.Collections;
using Zenject;

namespace RTS
{
    public interface IPlacedEvent
    {
        public void Placing();
    }

    public class ConstructBuild : BuildUnit, IPlacedEvent  // Produces units that are in the queue.
    {
        public GameObject complete;

        protected override void Complete(GameObject unit) => complete = unit;

        private Func<GameObject, Placing> place = obj => obj.GetComponent<Placing>();
        private Func<GameObject, UnitActivate> activate = obj => obj.GetComponent<UnitActivate>();

        public void PlacingCommand(ConstructCommand b)
        {
            GameObject construct = SpawnConstruct();

            b.placedUnit = place(construct);
            place(construct).placedEvent = b;
            place(construct).SetPlacing(true);
            StartCoroutine(Deactivate(construct));
        }

        private IEnumerator Deactivate(GameObject construct)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            activate(construct).Deactivate();
        }

        public GameObject SpawnAI()
        {
            GameObject construct = SpawnConstruct();

            place(construct).placedEvent = this;
            place(construct).SetPlacing(false);
            return construct;
        }

        public void Placing() => complete = null;

        public GameObject SpawnConstruct()
        {
            GameObject u = Spawn(complete);
            return u;
        }

        protected override void Selected(bool s) => MakeButtons(s);

        protected virtual void SelectedAlt(bool alt) => Selected(!alt);

        public void ClearButtons()
        {
            buttons.ToggleButtons(false);
            MakeButtons(true);
        }

        protected override void SetMovePoint()
        {

        }

        public void ClearComplete() => 
            money[team - 1].ChangeMoney(stats.Stats(complete).generalStats.cost);
    }
}