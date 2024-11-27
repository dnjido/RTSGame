using UnityEngine;

namespace RTS
{

    public class UnitTeam : MonoBehaviour
    {
        [SerializeField] private int _team;
        public int team { get => _team;
            private set { ChangeTeam(); _team = value; }
        }
        public TeamColors teamColor;

        public Material color;
        public int status;

        void Start() => SetTeam(_team);

        public void SetTeam(int t) => team = t;

        void OnValidate() => ValidateColor();

        public void ChangeTeam()
        {
            color = teamColor.materials[team - 1];
            SetStatus();
        }

        public void SetStatus()
        {
            gameObject.layer = 6 + team;
            status = 6 + team;
            SetColor();

            if (GetComponent<DetectEnemy>() == null) return;
            GetComponent<DetectEnemy>().layer = DetectLayers.Layers(this);
        }

        private void SetColor()
        {
            if (!GetComponent<ColorfulParts>()) return;

            ColorfulParts part = GetComponent<ColorfulParts>();
            foreach (GameObject parts in part.parts)
            {
                parts.GetComponent<Renderer>().material = color;
            }
        }

        private void ValidateColor()
        {
            if (!GetComponent<ColorfulParts>()) return;

            ColorfulParts part = GetComponent<ColorfulParts>();
            foreach (GameObject parts in part.parts)
            {
                parts.GetComponent<Renderer>().material = teamColor.materials[_team - 1];
            }
        }
    }
}
