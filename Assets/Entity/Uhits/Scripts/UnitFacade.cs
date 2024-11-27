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

    public class UnitFacade : MonoBehaviour, IUnitConstruct
    {
        [SerializeField] private float buildTime;
        [SerializeField] private UnitTransform unitTr;

        public void Construct(UnitTransform ut)
        {
            unitTr = ut;
            transform.position = unitTr.spawnPoint;
            transform.rotation = unitTr.rotate;
            //transform.LookAt(unitTr.end);
            GetComponent<UnitTeam>().SetTeam(unitTr.team);
        }

        public float GetBuildTime() => buildTime;

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

