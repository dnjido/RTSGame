namespace TBS
{
    public class Weapon
    {
        private readonly ProjectileCreator _projectileCreator;

        public Weapon(ProjectileCreator projectileCreator)
        {
            _projectileCreator = projectileCreator;
        }

        public void Attack()
        {
            _projectileCreator.Create();
            //Instantiate()
        }
    }
}
