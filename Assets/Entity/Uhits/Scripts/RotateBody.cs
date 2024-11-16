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
        //[SerializeField] private float speed;
        //private Quaternion initRot;
        private float initAngularSpeed;
        private NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            initAngularSpeed = agent.angularSpeed;
        }

        void Update() => Rotating();

        public void Rotating()
        {
            if (!GetComponent<DetectEnemy>()?.GetUnit()) { Return(); return; }
            GameObject unit = GetComponent<DetectEnemy>()?.GetUnit();

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
