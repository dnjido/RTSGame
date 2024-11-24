using UnityEngine;
using Zenject;

namespace RTS
{
    public interface IProjectileConstruct
    {
        public void Construct(ProjectileTransform pr);
    }

    public struct ProjectileTransform
    {
        public Vector3 start, end;
        public Quaternion rotate;
        public GameObject target;
        public float damage;

        public float Distance() => Vector3.Distance(start, end);
        public float Duration(float speed) => speed / Distance();
    }

    public class ProjectileFacade : MonoBehaviour, IProjectileConstruct
    {
        public void Construct(ProjectileTransform pr)
        {
            GetComponent<ProjectileMove>().StartMove(pr);
        }

        void Start()
        {

        }

        void Update()
        {

        }

        public class Factory : PlaceholderFactory<GameObject, ProjectileTransform, GameObject>
        {
        }
    }
}

