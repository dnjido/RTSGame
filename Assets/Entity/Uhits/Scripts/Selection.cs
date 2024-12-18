using UnityEngine;

namespace RTS
{
    public interface ISelecting
    {
        void Select();
    }

    public class Selection : MonoBehaviour // Stores information about the unit selection.
    {
        [SerializeField] private GameObject circle;
        public bool hasSelecting = true;
        public bool isSelected
        {
            get => _isSelected;
            set => Selecting(value);
        }

        private int team => GetComponent<UnitTeam>().team;

        public delegate void SelectedDelegate(bool select);
        public event SelectedDelegate SelectedEvent;

        private bool _isSelected;

        private void Selecting(bool s)
        {
            if (!hasSelecting || team != 1) return;

            _isSelected = s;
            circle.SetActive(s);
            SelectedEvent?.Invoke(s);
        }
    }
}