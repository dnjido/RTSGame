using UnityEngine;
using Zenject;

namespace RTS
{
    public class SpawnUnitAtStart : MonoBehaviour // Make unit at start
    {
        public int team => GetComponent<UnitTeam>().team;
        private UnitFacade.Factory unitFactory;
        [SerializeField] private Transform point;
        [SerializeField] private GameObject unit;
        private GameObject spawned;

        void Start() => Activate();

        [Inject]
        public void UnitFactory(UnitFacade.Factory f) =>
            unitFactory = f;

        public void Activate() =>
            spawned = Spawn(unit);

        public void Deactivate() =>
            Destroy(spawned);

        private GameObject Spawn(GameObject unit)
        {
            UnitTransform tr = SetUnit.Create(
                point.position,
                transform.rotation,
                team);
            return unitFactory.Create(unit, tr);
        }
    }
}

