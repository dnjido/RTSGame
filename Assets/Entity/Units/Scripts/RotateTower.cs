using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public class RotateTower : MonoBehaviour, IRotate // Rotate tower to target
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject tower;
        private Quaternion initRot;
        private GameObject target;

        private DetectEnemy detect => GetComponent<DetectEnemy>();

        void Start()
        {
            initRot = tower.transform.localRotation;
            OnEnable();
        }

        void OnEnable()
        {
            if (detect == null) return;
            detect.TargetEvent += SetTarget;
        }

        void OnDestroy() =>
            detect.TargetEvent -= SetTarget;

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
