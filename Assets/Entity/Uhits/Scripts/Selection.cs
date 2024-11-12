using UnityEditor;
using UnityEngine;

namespace RTS
{
    public class Selection : MonoBehaviour // Stores information about the unit selection.
    {
        [SerializeField] private GameObject circle;
        public bool isSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                circle.SetActive(value);
            }
        }
        private bool _isSelected;
    }
}