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
        // Start is called before the first frame update
        void Start()
        {
            moveStruct = new MoveStruct();
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            moveStruct.point = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (hasMove) Movement();
        }
        public void Movement()
        {
            agent.SetDestination(moveStruct.VectorSelector());
        }
        public void SetPoint(Vector3 p)
        {
            moveStruct.point = p;
        }
        public void SetUnit(GameObject u)
        {
            moveStruct.unit = u;
        }
    }
}