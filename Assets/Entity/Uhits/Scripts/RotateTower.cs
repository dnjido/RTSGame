using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace RTS
{
    public class RotateTower : MonoBehaviour, IRotate
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject tower;
        private Quaternion initRot;
        private GameObject target;

        void Start()
        {
            initRot = tower.transform.localRotation; 
            GetComponent<DetectEnemy>().TargetEvent += SetTarget;
        }

        void OnDestroy() =>
            GetComponent<DetectEnemy>().TargetEvent -= SetTarget;

        void Update() => Rotating();

        private void SetTarget(GameObject u) => target = u;

        public void Rotating()
        {
            if (!target) { Return(); return; }

            GameObject unit = target;

            tower.transform.rotation = RotateToObject.Rotate(unit.transform.position, tower.transform.position, tower.transform.rotation, speed);
        }

        public void Return()
        {
            if (tower.transform.localRotation == initRot) return;
            tower.transform.localRotation = Quaternion.Slerp(tower.transform.localRotation, initRot, speed * Time.deltaTime);
        }
    }
}
