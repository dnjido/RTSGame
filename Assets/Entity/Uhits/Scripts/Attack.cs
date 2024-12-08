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
        private bool hasFire = true, stopFire;
        private GameObject target;

        [Inject]
        public void UnitStats(GetStats g)
        {
            rate = g.Stats(gameObject).attackStats.attackRate;
            damage = g.Stats(gameObject).attackStats.damage;
            projectile = g.Stats(gameObject).attackStats.projectile;
        }

        [Inject]
        public void ProjectileFactory(ProjectileFacade.Factory f) =>
            projectileFactory = f;

        void OnEnable() =>
            GetComponent<DetectEnemy>().TargetEvent += SetTarget;

        void OnDisable() =>
            GetComponent<DetectEnemy>().TargetEvent -= SetTarget;

        void Update()
        {
            if (hasFire && target && !stopFire)
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

        public void SetStop(bool s) => stopFire = s;
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

