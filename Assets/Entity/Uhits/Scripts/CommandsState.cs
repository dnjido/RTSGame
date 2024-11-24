using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public interface IStart { void Start(); }

    public interface IUpdate { void Update(); }

    public interface IEnd { void End(); }

    public class StayState { }

    public class MoveState : IStart
    {
        readonly NavMeshAgent agent;
        readonly Vector3 point;

        public MoveState(NavMeshAgent a, Vector3 p) // Move command
        {
            agent = a;
            point = p;
            Start();
        }

        public void Start()
        {
            agent.SetDestination(point);
        }
    }

    public class FollowState : IStart, IUpdate // Follow command
    {
        public readonly NavMeshAgent agent;
        public readonly GameObject unit;
        private Vector3 oldPos;

        public FollowState(NavMeshAgent a, GameObject u)
        {
            agent = a;
            unit = u;
            Start();
        }

        public void Start() =>
            agent.SetDestination(unit.transform.position);

        public void Update()
        {
            if (!unit) return;

            if (!TargetMove.Move(oldPos, unit.transform.position, 0.01f)) return;

            oldPos = unit.transform.position;
            Start();
        }
    }

    public class AttackState : IStart, IUpdate // Attack command
    {
        public readonly NavMeshAgent agent;
        public readonly GameObject unit;
        public readonly DetectEnemy detector;
        private Vector3 oldPos;
        private bool HasTarget;

        public AttackState(NavMeshAgent a, GameObject u, DetectEnemy d)
        {
            agent = a;
            unit = u;
            detector = d;
            Start();
        }

        public void Start() =>
            agent.SetDestination(unit.transform.position);

        public void Update()
        {
            if (!unit) return;

            if (TargetMove.Move(oldPos, unit.transform.position, 0.01f))
            {
                oldPos = unit.transform.position;
                Start();
            }

            if (Vector3.Distance(agent.gameObject.transform.position, unit.transform.position) <= detector.range)
            {
                if (!HasTarget) SetTarget();
            }
            else HasTarget = false;
        }

        private void SetTarget()
        {
            agent.SetDestination(agent.gameObject.transform.position);
            HasTarget = true;
            detector.SetTarget(unit);
        }
    }

    public class MoveAttackState : IStart, IUpdate // Attack while moving command
    {
        public readonly NavMeshAgent agent;
        public readonly Vector3 point;
        public readonly DetectEnemy detector;
        private bool hasTarget;

        public MoveAttackState(NavMeshAgent a, Vector3 p, DetectEnemy d)
        {
            agent = a;
            point = p;
            detector = d;
            Start();
        }

        public void Start() =>
            agent.SetDestination(point);

        public void Update()
        {
            if (detector.GetUnit() != null && !hasTarget)
                HasTarget();

            if (detector.GetUnit() == null && hasTarget)
            {
                hasTarget = false;
                Start();
            }
        }

        private void HasTarget()
        {
            hasTarget = true;
            agent.SetDestination(agent.gameObject.transform.position);
        }
    }
}

