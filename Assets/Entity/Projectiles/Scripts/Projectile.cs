using UnityEngine;

namespace TBS 
{ 
    public class Projectile
    {
        private readonly ProjectileMove _projectileMove;

        public Projectile(ProjectileMove projectileMove)
        {
            _projectileMove = projectileMove;
        }
        public void Create()
        {
            //_projectileMove.Move(new Vector3(10, 3, 20));
        }
        public void Hit()
        {
            Debug.Log("Bullet Hit!");
        }
    }
}
