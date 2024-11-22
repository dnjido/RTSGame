using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public struct MoveStruct
    {
        public Vector3 point;
        public GameObject unit;
        public bool enemy;
        public Vector3 VectorSelector() => 
            unit ? unit.transform.position : point;
        public Vector3 IsEnemy(float distance, GameObject g) => 
            enemy ? unit.transform.position - unit.transform.forward * distance : unit.transform.position;
    }

    public class UnitMovement : MonoBehaviour // Moving a unit to a point or to another unit.
    {
        public bool hasMove;
        [SerializeField] private float speed;
        [SerializeField] private float rangeStop, rangeAttack;
        private NavMeshAgent agent;
        private MoveStruct moveStruct;
        IUpdate updater;

        void Start()
        {
            moveStruct = new MoveStruct();
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            moveStruct.point = transform.position;
        }

        void FixedUpdate() => StateUpdater();

        private void StateUpdater() 
        { 
            if (updater != null) updater.Update();
        }

        private float GetDistance() => Vector3.Distance(transform.position, moveStruct.unit.transform.position);

        private void SetPoint(Vector3 p)
        {
            moveStruct.unit = null;
            moveStruct.enemy = false;
            moveStruct.point = p;

            if (Input.GetKey("a")) 
                updater = new MoveAttackState(agent, moveStruct.point, GetComponent<DetectEnemy>());
            else
            {
                updater = null;
                new MoveState(agent, moveStruct.point);
            }
        }

        private void SetUnit(GameObject u)
        {
            moveStruct.unit = u;
            moveStruct.enemy = u.GetComponent<UnitTeam>().team.team != 1;

            if(!moveStruct.enemy)
                updater = new FollowState(agent, moveStruct.unit);
            else
                updater = new AttackState(agent, moveStruct.unit, GetComponent<DetectEnemy>());
        }

        public void Command()
        {
            GameObject ray = CursorRay.RayUnit();

            if (ray != null && ray.tag == "Unit")
                SetUnit(ray);
            else
                SetPoint(CursorRay.RayPoint());
        }
    }

    public class TargetMove
    {
        public static bool Move(Vector3 old, Vector3 _new, float tolerance) =>
            Vector3.Distance(old, _new) > tolerance;
    }
}