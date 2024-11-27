using DG.Tweening;
using UnityEngine;

namespace RTS 
{
    public class ProjectileMove : MonoBehaviour // Projectile movement to a point
    {
        [SerializeField] private float speed;
        private ProjectileTransform projTr;

        public void StartMove(ProjectileTransform pt)
        {
            projTr = pt;
            transform.position = projTr.start;
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
