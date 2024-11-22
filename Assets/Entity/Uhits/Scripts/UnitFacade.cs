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
        }

        public float GetBuildTime() => buildTime;

        public class Factory : PlaceholderFactory<GameObject, UnitTransform, GameObject>
        {
        }
    }

    public class SetUnit
    {
        public static UnitTransform Create(Vector3 point, Quaternion rotate)
        {
            UnitTransform tr = new UnitTransform();
            tr.spawnPoint = point;
            tr.rotate = rotate;
            return tr;
        }
    }
}

