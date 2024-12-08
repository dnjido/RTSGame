using UnityEngine;
using UnityEngine.AI;
using Zenject;

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
        [SerializeField]private float speed;

        public bool hasMove;
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

        [Inject]
        public void UnitStats(GetStats g)
        {
            speed = g.Stats(gameObject).moveStats.speed;
        }

        void FixedUpdate() => StateUpdater();

        private void StateUpdater() 
        { 
            if (updater != null) updater.Update();
        }

        public virtual void SetPoint(Vector3 p)
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
                updater = new AttackState(agent, moveStruct.unit);
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