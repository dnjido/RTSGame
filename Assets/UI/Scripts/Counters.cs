using UnityEngine;

namespace RTS
{
    public abstract class Counters : MonoBehaviour
    {
        protected PlayerResources[] playerResources;
        protected PlayerResources currentResource => playerResources[ID];
        protected abstract float resource { get; }
        protected int ID;

        public abstract void GetMoney(PlayerResources[] r);
        protected abstract void SetCount();
    }
}
