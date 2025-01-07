using System;
using UnityEngine;
using Zenject;

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
            set { _team = value; ChangeTeam(); }
        }
        public TeamColors teamColor;

        public Material color;
        public int relationship;

        private Relationship[] _relationships;

        private ColorfulParts part => GetComponent<ColorfulParts>();
        private DetectEnemy detect => GetComponent<DetectEnemy>();

        void Awake()
        {
            SetTeam(_team); //if (!GetComponent<Placing>()) 
        }

        [Inject]
        public void Relationship(Relationship[] r) =>
            _relationships = r;

        public void Activate() => SetTeam(trTeam != 0 ? trTeam : _team);

        public void SetTeam(int t) => team = t;

        void OnValidate() => ValidateColor();

        public void ChangeTeam()
        {
            color = teamColor.materials[team - 1];
            SetStatus();
            SetColor();
        }

        public void SetStatus()
        {
            relationship = _relationships[team - 1].relationship;
            //_relationships = null;

            SetLayer();

            if (detect) detect.layer = DetectLayers.Layers(this);
        }

        private void SetColor()
        {
            if (!part) return;

            foreach (GameObject parts in part.parts) 
                parts.GetComponent<Renderer>().material = color;

            GetComponent<MapMarker>()?.SetColor(color.GetColor("_Color"));
        }

        private void ValidateColor()
        {
            if (!part) return;
            int tm = _team == 0 ? 1 : _team;
            color = teamColor.materials[tm - 1];

            foreach (GameObject parts in part.parts) 
                parts.GetComponent<Renderer>().material = color;

            GetComponent<MapMarker>()?.SetColor(color.GetColor("_Color"));
        }

        public void SetLayer() => gameObject.layer = 6 + team;

        public void RemoveLayer() => gameObject.layer = 1 << 2;
    }
}
