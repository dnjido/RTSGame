using UnityEngine;
using Zenject;

namespace RTS
{
    public class RightClick : MonoBehaviour // Handles clicks.
    {
        private MoveCommand moveTo;

        [Inject]
        public void Init(MoveCommand m)
        {
            moveTo = m;
        }

        void Update() 
        {
            if (Input.GetMouseButtonDown(1)) moveTo.MoveTo();
        }
    }

    public class MoveCommand
    {
        readonly SelectedUnits selectedUnits;

        MoveCommand(SelectedUnits su) 
        {
            selectedUnits = su;
        }

        public void MoveTo()
        {
            if (CursorOnUI.CursorOverUI()) return;
            if (selectedUnits?.selected.units.Length <= 0) return;

            foreach (GameObject unit in selectedUnits.selected.units)
            {
                if(unit != null) SetTarget(unit);
            }
        }

        public void SetTarget(GameObject obj)
        {
            UnitMovement um = obj.GetComponent<UnitMovement>();
            um.Command();
        }
    }
}
