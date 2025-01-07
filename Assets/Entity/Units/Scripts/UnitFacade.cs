using UnityEngine;
using Zenject;

namespace RTS
{
    public interface IUnitConstruct
    {
        public void Construct(UnitTransform ut);
    }

    public struct UnitTransform
    {
        public Vector3 spawnPoint;
        public Quaternion rotate;
        public GameObject target;
        public int team;
    }

    public class UnitFacade : MonoBehaviour, IUnitConstruct // Sets up main unit characteristics
    {
        [SerializeField] private StatsCategoryList statsCategory = new StatsCategoryList();

        [SerializeField] private int ID;

        public (int, int) GetIDs() => (ID, (int)statsCategory);

        public float buildTime;
        public float cost;

        public float costPerTick => buildTime * Time.deltaTime;

        [SerializeField] private UnitTarget unitTarget;
        public int getTarget => (int)unitTarget;
        public int getBitwiseTarget => 1 << (int)unitTarget;

        [SerializeField] public string[] unitType;
        [SerializeField] public UnitTransform unitTr;

        public GetStats stats { get; private set; }

        [Inject]
        public void UnitStats(GetStats g)
        {
            stats = g;
            unitTarget = g.Stats(gameObject).generalStats.targetType;
            buildTime = g.Stats(gameObject).generalStats.buildTime;
            cost = g.Stats(gameObject).generalStats.cost;
        }

        public void Construct(UnitTransform ut)
        {
            unitTr = ut;
            transform.position = unitTr.spawnPoint;
            transform.rotation = unitTr.rotate;
            //GetComponent<UnitTeam>().SetTeam(unitTr.team);
            GetComponent<UnitTeam>().team = (unitTr.team);
        }

        public class Factory : PlaceholderFactory<GameObject, UnitTransform, GameObject>
        {
        }
    }

    public class SetUnit
    {
        public static UnitTransform Create(Vector3 point, Quaternion rotate, int t)
        {
            UnitTransform tr = new UnitTransform();
            tr.spawnPoint = point;
            tr.rotate = rotate;
            tr.team = t;
            return tr;
        }
    }
}

