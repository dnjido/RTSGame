using UnityEngine;
using Zenject;

namespace RTS
{
    public class ApplyAction : MonoBehaviour
    {
        private PlayerAction action;

        private GameObject unit => CursorRay.RayUnit();
        private bool onUI => CursorOnUI.CursorOverUI();

        [Inject]
        public void Init(PlayerAction a) =>
            action = a;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !onUI) Apply();
            if (Input.GetMouseButtonDown(1) && !onUI) Clear();
        }

        public void Apply() => action.Apply(unit);
        public void Clear()
        {
            action.Cancel();
            CursorIcon.Clear();
        }
    }
}
