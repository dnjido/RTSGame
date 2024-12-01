using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public struct MoveStruct
    {
        public Vector3 point;
        public GameObject unit;
        public bool enemy;
    }

    public class UnitMovement : MonoBehaviour // Moving a unit to a point or to another unit.
    {
        public bool hasMove;
        [SerializeField] private float speed;
        [SerializeField] private float rangeStop, rangeAttack;
        protected NavMeshAgent agent;
        protected MoveStruct moveStruct;
        protected IState updater;

        protected virtual void Start()
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

        public void SetPoint(Vector3 p)
        {
            moveStruct.unit = null;
            moveStruct.enemy = false;
            moveStruct.point = p;

            if (Input.GetKey("a")) 
                updater = new MoveAttackState(agent, moveStruct.point, GetComponent<DetectEnemy>());
            else
                updater = new MoveState(agent, moveStruct.point);
        }

        protected virtual void SetUnit(GameObject u)
        {
            moveStruct.unit = u;
            moveStruct.enemy = u.GetComponent<UnitTeam>().team != 1;

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