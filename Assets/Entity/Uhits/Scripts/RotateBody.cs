using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public interface IRotate
    {
        public void Rotating();
        public void Return();
    }

    public class RotateBody : MonoBehaviour, IRotate
    {
        private float initAngularSpeed;
        private NavMeshAgent agent;
        private GameObject target;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            initAngularSpeed = agent.angularSpeed;
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
