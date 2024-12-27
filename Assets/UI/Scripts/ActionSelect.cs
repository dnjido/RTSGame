using UnityEngine;
using Zenject;

namespace RTS
{
    public abstract class ActionSelect : MonoBehaviour
    {
        [SerializeField] protected int team;
        [SerializeField] protected Texture2D icon;
        protected PlayerAction action;

        [Inject]
        public void Init(PlayerAction a) =>
            action = a;

        public abstract void Press();
    }
}