using DG.Tweening;
using UnityEngine;

namespace TBS
{
    public class ProjectileMove : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector3 _dir;

        public void MoveBus(ProjMoveBus projMoveBus)
        {
            //_dir = projMoveBus._dir;
            projMoveBus._dir.y = _dir.y;
            _dir = projMoveBus._dir;
            transform.DOMove(_dir, speed).OnComplete(End);
            print(gameObject.name + "Moved");
        }

        public void Move(Vector3 dir)
        {
            dir.y = _dir.y;
            _dir = dir;
            transform.DOMove(_dir, speed).OnComplete(End);
        }

        private void End() => Destroy(gameObject);
    }
}
