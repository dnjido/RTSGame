using UnityEngine;

namespace RTS
{
    public struct TeamNumber
    {
        public int team;
        public string status;
    }

    public class UnitTeam : MonoBehaviour
    {
        [SerializeField] private int _team;
        public TeamNumber team;

        void Start() => ChangeTeam();

        public void ChangeTeam()
        {
            team = new TeamNumber();
            team.team = _team;
            SetStatus();
        }

        public void SetStatus()
        {
            gameObject.layer = 6 + team.team;
        }
    }
}
