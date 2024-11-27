using System.Collections;
using UnityEngine;
using Zenject;

namespace RTS
{
    public class Attack : MonoBehaviour // Attack enemy
    {
        [SerializeField] private float rate, damage;
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform firePoint;
        ProjectileFacade.Factory projectileFactory;
        private bool hasFire = true;
        private GameObject target;

        [Inject]
        public void ProjectileFactory(ProjectileFacade.Factory f) =>
            projectileFactory = f;

        void Start() =>
            GetComponent<DetectEnemy>().TargetEvent += SetTarget;

        void OnDestroy() =>
            GetComponent<DetectEnemy>().TargetEvent -= SetTarget;

        void Update()
        {
            if (hasFire && target)
                StartCoroutine(FireCoroutine());
        }

        private void SetTarget(GameObject u) => target = u;

        private void Fire()
        {
            ProjectileTransform tr = SetProjectile.Create(damage, target, transform.position);
            GameObject p = projectileFactory.Create(projectile, tr);
            hasFire = false;
        }

        IEnumerator FireCoroutine()
        {
            Fire();
            yield return new WaitForSeconds(rate);
            hasFire = true;
        }
    }

    public class SetProjectile
    {
        public static ProjectileTransform Create(float damage, GameObject enemy, Vector3 point)
        {
            ProjectileTransform tr = new ProjectileTransform();
            tr.target = enemy;
            tr.start = point;
            tr.end = enemy.transform.position;
            tr.damage = damage;
            return tr;
        }
    }
}

