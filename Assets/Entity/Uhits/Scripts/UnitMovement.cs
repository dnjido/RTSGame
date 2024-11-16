using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    struct MoveStruct
    {
        public Vector3 point;
        public GameObject unit;
        public Vector3 VectorSelector() => 
            unit ? unit.transform.position : point;
    }

    public class UnitMovement : MonoBehaviour // Moving a unit to a point or to another unit.
    {
        public bool hasMove;
        [SerializeField] private float speed;
        private NavMeshAgent agent;
        private MoveStruct moveStruct;

        void Start()
        {
            moveStruct = new MoveStruct();
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            moveStruct.point = transform.position;
        }

        void FixedUpdate() => Movement();

        private void Movement()
        {
            if (hasMove) 
                agent.SetDestination(moveStruct.VectorSelector());
        }
        private void SetPoint(Vector3 p)
        {
            //print(moveStruct.VectorSelector());
            moveStruct.unit = null;
            moveStruct.point = p;
        }
        private void SetUnit(GameObject u)
        {
            moveStruct.unit = u;
        }
        public void Command()
        {
            GameObject ray = CursorRay.RayUnit();

            if (ray != null && ray.tag == "Unit")
                SetUnit(CursorRay.RayUnit());
            else
                SetPoint(CursorRay.RayPoint());
        }
    }
}