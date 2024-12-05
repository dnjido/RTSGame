namespace TBS
{
    public class Player
    {
        private readonly Weapon _weapon;

        public Player(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void Attack()
        {
            _weapon.Attack();
        }
    }
}
