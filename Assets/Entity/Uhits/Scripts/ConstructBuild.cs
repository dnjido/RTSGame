using UnityEngine;
using System.Linq;
using System;

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

        public void PlacingCommand(ConstructCommand b)
        {
            GameObject construct = SpawnConstruct();

            b.placedUnit = place(construct);
            place(construct).SetPlacing(true);
            place(construct).placedEvent = b;
        }

        public void Placing() => complete = null;

        public GameObject SpawnConstruct()
        {
            GameObject u = Spawn(complete);
            return u;
        }

        protected virtual void SelectedAlt(bool alt)
        {
            Selected(!alt);
        }

        public void ClearButtons()
        {
            buttons.Buttons(false);
            MakeButtons(true);
        }
    }
}