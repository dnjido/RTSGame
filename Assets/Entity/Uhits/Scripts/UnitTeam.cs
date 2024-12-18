using UnityEngine;
namespace RTS
{
    public interface IUnitTeam
    {
        public int team {  get; set; }
    }

    public class UnitTeam : MonoBehaviour, IUnitTeam // Unit team that sets up color and detect layer
    {
        private int trTeam => GetComponent<UnitFacade>().unitTr.team;
        [SerializeField] private int _team;
        public int team { get => _team;
            set { ChangeTeam(); _team = value; }
        }
        public TeamColors teamColor;

        public Material color;
        public int status;

        void Start() 
        {
            if(!GetComponent<Placing>()) SetTeam(_team);
        }

        public void Activate() => SetTeam(trTeam != 0 ? trTeam : _team);

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
            GetComponent<MapMarker>()?.SetColor(color.GetColor("_Color"));
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
