using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public interface IRotate
    {
        public void Rotating();
        public void Return();
    }

    public class RotateBody : MonoBehaviour, IRotate // Rotate unit to target
    {
        private float initAngularSpeed;
        private GameObject target;

        private DetectEnemy detect => GetComponent<DetectEnemy>();
        private NavMeshAgent agent => GetComponent<NavMeshAgent>();

        void Start()
        {
            initAngularSpeed = agent.angularSpeed;
            //detect.TargetEvent += SetTarget;
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

            agent.angularSpeed = 0;

            transform.rotation = RotateToObject.Rotate(unit.transform.position, transform.position, transform.rotation, initAngularSpeed);
        }

        public void Return()
        {
            if (agent.angularSpeed == initAngularSpeed) return;
            agent.angularSpeed = initAngularSpeed;
        }
    }
}
