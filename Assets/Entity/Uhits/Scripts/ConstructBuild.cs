using UnityEngine;

namespace RTS
{

    public class ConstructBuild : BuildUnit  // Produces units that are in the queue.
    {
        public GameObject complete;

        protected override void Complete(GameObject unit) => complete = unit;

        public void Placing(ConstructCommand b)
        {
            GameObject u = Spawn(complete);
            Placing(u);
            b.placedUnit = u.GetComponent<Placing>();
            u.GetComponent<Placing>().PlaceEvent += b.Placing;
        }

        public void Placing(GameObject u) =>
            u.GetComponent<Placing>().SetPlacing(true);
    }
}