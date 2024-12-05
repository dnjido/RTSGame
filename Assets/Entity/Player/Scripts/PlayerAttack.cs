using UnityEngine;
using Zenject;

namespace TBS
{
    public class PlayerAttack : MonoBehaviour
    {
        private Player _player;

        [Inject]
        public void Construct(Player player) => _player = player;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _player.Attack();
            }
        }
    }
}