using UnityEngine;

namespace RTS
{
    public struct TeamNumber
    {
        public int team;
        public int status;
        public Material color;
    }

    public class UnitTeam : MonoBehaviour
    {
        //[SerializeField] private int _team { 
        //    get => team.team;
        //    set 
        //    {
        //        team.team = value;
        //        SetStatus();
        //    }
        //}
        [SerializeField] private int _team;
        public TeamNumber team;
        public TeamColors color;

        void Start() => ChangeTeam();

        //private void OnValidate() { if () ChangeTeam(); } 
        void OnValidate() => ValidateColor();

        public void ChangeTeam()
        {
            team = new TeamNumber();
            team.team = _team;
            team.color = color.materials[team.team - 1];
            SetStatus();
        }

        public void SetStatus()
        {
            gameObject.layer = 6 + team.team;
            team.status = 6 + team.team;
            SetColor();
        }

        private void SetColor()
        {
            if (!GetComponent<ColorfulParts>()) return;

            ColorfulParts part = GetComponent<ColorfulParts>();
            foreach (GameObject parts in part.parts)
            {
                parts.GetComponent<Renderer>().material = team.color;
            }
        }

        private void ValidateColor()
        {
            if (!GetComponent<ColorfulParts>()) return;

            ColorfulParts part = GetComponent<ColorfulParts>();
            foreach (GameObject parts in part.parts)
            {
                parts.GetComponent<Renderer>().material = color.materials[_team - 1];
            }
        }
    }
}
