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
            set
            {
                if (!hasSelecting) return;
                _isSelected = value;
                circle.SetActive(value);
                SelectedEvent?.Invoke(value);
            }
        }

        public delegate void SelectedDelegate(bool select);
        public event SelectedDelegate SelectedEvent;

        private bool _isSelected;
    }
}