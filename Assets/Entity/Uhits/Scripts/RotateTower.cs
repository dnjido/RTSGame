using UnityEngine;

namespace RTS
{
    public class RotateTower : MonoBehaviour, IRotate
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject tower;
        private Quaternion initRot;

        void Start()
        {
            initRot = tower.transform.localRotation;
        }

        void Update() => Rotating();

        public void Rotating()
        {
            if (!GetComponent<DetectEnemy>()?.GetUnit()) { Return(); return; }

            GameObject unit = GetComponent<DetectEnemy>()?.GetUnit();

            tower.transform.rotation = RotateToObject.Rotate(unit.transform.position, tower.transform.position, tower.transform.rotation, speed);
        }

        public void Return()
        {
            if (tower.transform.localRotation == initRot) return;
            tower.transform.localRotation = Quaternion.Slerp(tower.transform.localRotation, initRot, speed * Time.deltaTime);
        }
    }
}
