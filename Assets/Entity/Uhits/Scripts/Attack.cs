using NUnit;
using RTS;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RTS
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float rate, damage;
        [SerializeField] private GameObject proj;
        [SerializeField] private GameObject firePoint;
        private bool hasFire = true;

        // Update is called once per frame
        void Update()
        {
            if (hasFire && GetComponent<DetectEnemy>()?.GetUnit())
                StartCoroutine(FireCoroutine());
        }

        private void Fire()
        {
            GameObject p = Instantiate(proj, firePoint.transform.position, firePoint.transform.rotation);
            ProjectileTransform tr = SetProjectile.Create(damage, 
                GetComponent<DetectEnemy>().GetUnit(), 
                firePoint.transform.position);

            p.GetComponent<ProjectileMove>().StartMove(tr);
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

