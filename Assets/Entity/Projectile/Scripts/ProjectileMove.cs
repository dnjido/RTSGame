using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace RTS 
{
    public struct ProjectileTransform
    {
        public Vector3 start, end;
        public GameObject target;
        public Quaternion rotate;
        public float damage;

        public float Distance() => Vector3.Distance(start, end);
        public float Duration(float speed) => speed / Distance();
    }

    public class ProjectileMove : MonoBehaviour // Projectile movement to a point
    {
        [SerializeField] private float speed;
        private ProjectileTransform projTr;

        public void StartMove(ProjectileTransform pt)
        {
            projTr = pt;
            transform.LookAt(projTr.end);
            transform.DOMove(projTr.end, projTr.Duration(speed)).SetEase(Ease.Linear).OnComplete(Damage);
        }

        private void Damage()
        {
            if(projTr.target) 
                projTr.target.GetComponent<IDamage>().ApplyDamage(projTr.damage);
            Destroy(gameObject);
        }
    }
}
